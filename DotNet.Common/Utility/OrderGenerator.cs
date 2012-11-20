// -----------------------------------------------------------------------
// <copyright file="OrderGenerator.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// 排序序号生成器
    /// </summary>
    public class OrderGenerator
    {
        private const String OrderChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static long _lastOrder;

        private OrderGenerator()
        {
        }
        /// <summary> 生成一个新的、递增的顺序号</summary>
        /// <returns> 返回顺序号
        /// </returns>
        public static long NewOrder()
        {
            //表示自0001年1月1日午夜12:00:00以来已经过的时间的以100毫微秒为间隔的间隔数。
            var newOrder = DateTime.Now.Ticks >> 12;
            while (true)
            {
                var oldOrder = _lastOrder;
                if (newOrder <= oldOrder)
                    newOrder = oldOrder + 1;
                //Interlocked.CompareExchange,原子操作
                //lastOrder与oldOrder比较，如果两者相等，返回newOrder
                if (Interlocked.CompareExchange(ref _lastOrder, newOrder, oldOrder) == oldOrder)
                {
                    //成功将lastOrder修改为newOrder
                    return newOrder;
                }
                //继续下一次尝试
            }
        }

        /// <summary> 生成一个新的、递增的顺序号字符串</summary>
        /// <returns> 返回顺序号字符串，长度为10
        /// </returns>
        public static string NewStringOrder()
        {
            var order = NewOrder();
            var buffer = new char[10];
            var index = 9;
            while (order > 0 && index >= 0)
            {
                buffer[index--] = OrderChars[(int)(order % 36)];
                order = order / 36;
            }
            while (index >= 0)
                buffer[index--] = '0';
            return new string(buffer);
        }

        /// <summary> 生成一个新的、递增的顺序号字符串</summary>
        public static string NewStringOrder(int length)
        {
            if (length <= 0) return string.Empty;
            var order = NewOrder();
            var buffer = new char[length];
            var index = length - 1;
            while (order > 0 && index >= 0)
            {
                buffer[index--] = OrderChars[(int)(order % 36)];
                order = order / 36;
            }
            while (index >= 0)
                buffer[index--] = '0';
            return new string(buffer);
        }
    }
}
