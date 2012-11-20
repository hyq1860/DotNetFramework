
using System;
using System.Collections.ObjectModel;

namespace DotNet.Common.Collections
{
    /// <summary>
	/// 提供集合键嵌入在实现 <see cref="INamedObject"/> 集合子项中的<b>Key</b>属性的集合类。 
    /// </summary>
    /// <typeparam name="T">带有集合中的键属性的集合子项。</typeparam>
    public class NamedCollection<T> : KeyedCollection<string, T>
        where T : INamedObject
    {

        /// <summary>
        /// 初始化使用默认相等比较器的 <b>KeyedCollection</b> 类的新实例。 
        /// </summary>
        /// <remarks>字符串键值不区分大小写。</remarks>
        public NamedCollection()
            : base(StringComparer.InvariantCultureIgnoreCase)
        {
        }

        /// <summary>
        /// 初始化使用指定字符串相等比较器的 <b>KeyedCollection</b> 类的新实例。 
        /// </summary>
        /// <param name="comparer">比较键时要使用的 <see cref="StringComparer"/>。</param>
		public NamedCollection(StringComparer comparer)
            : base(comparer ?? StringComparer.InvariantCultureIgnoreCase)
        {
        }

        /// <summary>
        /// 从指定元素提取键。
        /// </summary>
        /// <param name="item">从中提取键的元素。</param>
        /// <returns>指定元素的键。</returns>
        protected override string GetKeyForItem(T item)
        {
            return item.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, T item)
        {
			if (this.Contains(item.Name))
                throw new ArgumentException("DuplicateKeyException" + item.Name);

            base.InsertItem(index, item);
        }

        /// <summary>
        /// Adds the query string.
        /// If the specified name already exists, the previous value will be replaced.
        /// </summary>
        /// <param name="item"></param>
        public void AddAndReplace(T item)
        {
            if (item == null)
                return;

			if (base.Contains(item.Name))
				base.Remove(item.Name);

            base.Add(item);
        }

        /// <summary>
        /// get
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
		public new T this[string name]
        {
            get
            {
                T item = default(T);
				if (Contains(name))
					item = base[name];

                return item;
            }
            set
            {
                AddAndReplace(value);
            }
        }
    }
}