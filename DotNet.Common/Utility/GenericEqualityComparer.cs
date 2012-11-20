// -----------------------------------------------------------------------
// <copyright file="GenericEqualityComparer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.Common.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Linq.Expressions;
    using System.Reflection;

     /// <summary>
     /// 通用model的逻辑上相等性比较器
     /// </summary>
     /// <typeparam name="T"></typeparam>
     public class GenericEqualityComparer<T>:IEqualityComparer<T>
     {
         private static readonly Func<T, T, bool> s_equal;
 
         private static readonly Func<T, int> s_hashCode;
 
         static GenericEqualityComparer()
         {
             var x = Expression.Parameter(typeof(T), "x");
             var y = Expression.Parameter(typeof(T), "y");
 
             var readableProps =from prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 where prop.CanRead
                                 select prop;
             if (readableProps.Count() == 0)
             {
                 s_equal = (t1, t2) => false;
                 s_hashCode = t1 => 0;
             }
             else
             {
                 #region 动态生成Equals委托
                 // x.p == y.p 
                 var equalElements = readableProps.Select(p => (Expression)Expression.Equal(Expression.Property(x, p), Expression.Property(y, p)));
                 var equalFunBody = equalElements.Aggregate((a, b) => Expression.AndAlso(a, b));
                 s_equal = Expression.Lambda<Func<T, T, bool>>(equalFunBody, x, y).Compile();
 
                 #endregion
 
                 #region 动态生成GetHashCode的委托
 
                 // Object.ReferenceEquals(x.A, null)?0:obj.A.GetHashCode() ^ Object.ReferenceEquals(x.B, null)?0:obj.A.GetHashCode()
                 MethodInfo miGetHashCode = typeof(object).GetMethod("GetHashCode");
                 MethodInfo miReferenceEquals = typeof(object).GetMethod("ReferenceEquals");
                 //Object.ReferenceEquals(x.A, null)?0:obj.A.GetHashCode() arrays
                 var elements = readableProps.Select(p =>
                 {
                     var propertyExp = Expression.Property(x, p);//x.A
                     var testExp = Expression.Call(null, miReferenceEquals, Expression.Convert(propertyExp, typeof(object)), Expression.Constant(null));
 
                     return (Expression)Expression.Condition(
                                                                 testExp
                                                               , Expression.Constant(0, typeof(int))
                                                               , Expression.Call(propertyExp, miGetHashCode)
                                                             );
                 });
                 //aggregate element by ExclusiveOr
                 var body = elements.Aggregate((a, b) => Expression.ExclusiveOr(a, b));
                 //create an lambdaExpression and compile to delegate
                 s_hashCode = Expression.Lambda<Func<T, int>>(body, x).Compile();
 
                 #endregion
             }
         }
 
         /// <summary>
         /// 判断两个model，是否逻辑上相等
         /// </summary>
         /// <param name="x"></param>
         /// <param name="y"></param>
         /// <returns></returns>
         public static bool Equals(T x, T y)
         {
             //Check whether the compared objects reference the same data.
             if (Object.ReferenceEquals(x, y)) return true;
 
             //Check whether any of the compared objects is null.
             if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                 return false;
 
             //Check whether the products' properties are equal.
             return s_equal(x, y);
         }
 
         /// <summary>
         /// 获取model对象业务逻辑上的HashCode
         /// </summary>
         /// <param name="obj"></param>
         /// <returns></returns>
         public static int GetHashCode(T obj)
         {
             //Check whether the object is null
             if (Object.ReferenceEquals(obj, null)) return 0;
             return s_hashCode(obj);
         }
 
         bool IEqualityComparer<T>.Equals(T x, T y)
         {
             return Equals(x, y);
         }
 
         int IEqualityComparer<T>.GetHashCode(T obj)
         {
             return GetHashCode(obj);
         }
     }
}
