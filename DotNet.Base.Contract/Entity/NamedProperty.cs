using System;

namespace DotNet.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class NamedProperty<T>
    {
        /// <summary>
        /// 属性名
        /// </summary>
        private string _name;

        /// <summary>
        /// 属性默认值
        /// </summary>
        private T _defaultValue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="defaultValue">属性默认值</param>
        private NamedProperty(string name,T defaultValue)
        {
            _name = name;
            _defaultValue = defaultValue;
        }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// 属性的默认值
        /// </summary>
        public T DefaultValue
        {
            get { return _defaultValue; }
        }

        /// <summary>
        /// 创建一个命名属性对象
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="defaultVlaue">属性默认值</param>
        /// <returns>命名属性对象</returns>
        public static NamedProperty<T> Create(string name, T defaultVlaue)
        {
            return new NamedProperty<T>(name, defaultVlaue);
        }

        /// <summary>
        /// 重写命名属性类对象的判断函数
        /// </summary>
        /// <param name="obj">命名属性类对象</param>
        /// <returns>true:对象属性名称相等;false:对象属性名称不等;</returns>
        public override bool Equals(object obj)
        {
            NamedProperty<T> namedProperty = obj as NamedProperty<T>;
            if(namedProperty!=null)
            {
                return namedProperty.Name.Equals(this.Name);
            }
            return false;
        }

        /// <summary>
        /// 重写命名属性类对象的获取哈希码函数
        /// </summary>
        /// <returns>命名属性类对象的属性名称的哈希码</returns>
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
