/*******************************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author: Allen Wang(Allen.G.Wang@newegg.com) 
 * Create Date: 12/23/2008 
 * Description:
 *          
 * Revision History:
 *      Date         Author               Description
 * 
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

using DotNet.Common.Utility.TypeResolution;

namespace DotNet.Common.Utility
{
    /// <summary>
    /// 类型辅助类。
    /// </summary>
    public static class TypeHelper
    {
		#region [ Fields ]

		private static readonly ITypeResolver _internalTypeResolver = new CachedTypeResolver(new TypeResolver());

		#endregion

		#region [ Methods ]

		/// <summary>
		/// Resolves the supplied type name into a <see cref="System.Type"/>
		/// instance.
		/// </summary>
		/// <param name="typeName">
		/// The (possibly partially assembly qualified) name of a
		/// <see cref="System.Type"/>.
		/// </param>
		/// <returns>
		/// A resolved <see cref="System.Type"/> instance.
		/// </returns>
		/// <exception cref="System.TypeLoadException">
		/// If the type cannot be resolved.
		/// </exception>
		public static Type ResolveType(string typeName)
		{
			Type type = TypeRegistry.ResolveType(typeName);
			if (type == null)
			{
				type = _internalTypeResolver.Resolve(typeName);
			}
			return type;
		}

		/// <summary>
		/// 从类型字符串中解析出不含有AssemblyName的类型全名称。
		/// </summary>
		/// <param name="typeName">类型字符串</param>
		/// <returns>类型全名称。</returns>
		public static string GetTypeName(string typeName)
		{
			TypeAssemblyInfo typeAssemblyInfo = new TypeAssemblyInfo(typeName);
			return typeAssemblyInfo.TypeName;
		}

		/// <summary>
        /// 
        /// </summary>
        /// <param name="theType"></param>
        /// <returns></returns>
        public static bool IsNullableType(Type theType)
        {
            return (theType.IsGenericType && !theType.IsGenericTypeDefinition) 
                && (theType.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool TypeAllowsNullValue(Type type)
        {
            // Only reference types and Nullable<> types allow null values
            return (!type.IsValueType ||
                (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)));
        }
        
        /// <summary>
        /// 判断类型是否是枚举类型。
        /// </summary>
        /// <param name="theType"></param>
        /// <returns>如果 <paramref name="theType"/> 是枚举类型返回 true, 反之返回 false.</returns>
        public static bool IsEnumType(Type theType)
        {
			if (theType == null)
			{
				return false;
			}
			
			return theType.IsEnum;
		}

		/// <summary>
		/// Returns an array of Type objects corresponding
		/// to the type of parameters provided.
		/// </summary>
		/// <param name="parameters">
		/// Parameter values.
		/// </param>
		public static Type[] GetParameterTypes(params object[] parameters)
		{
			List<Type> result = new List<Type>();

			if (parameters == null)
			{
				result.Add(typeof(object));
			}
			else
			{
				foreach (object item in parameters)
				{
					if (item == null)
					{
						result.Add(typeof(object));
					}
					else
					{
						result.Add(item.GetType());
					}
				}
			}

			return result.ToArray();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static string GetParameterTypesString(params object[] parameters)
		{
			StringBuilder sb = new StringBuilder();
			Type[] parameterTypes = GetParameterTypes(parameters);
			foreach (Type type in parameterTypes)
			{
				if (sb.Length > 0)
				{
					sb.Append(", ");
				}

				sb.Append(type.Name);
			}

			return sb.ToString();
		}

		#endregion
	}
}