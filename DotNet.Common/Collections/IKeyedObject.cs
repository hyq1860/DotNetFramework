namespace DotNet.Common.Collections
{
    /// <summary>
    /// 表示带有集合中的键属性的集合子项。
    /// </summary>
    public interface IKeyedObject
    {
        /// <summary>
        /// 获取集合的键值。
        /// </summary>
        /// <value>集合的键值。</value>
        string Key { get; }
    }
}