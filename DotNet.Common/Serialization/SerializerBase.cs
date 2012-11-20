using System;
using System.IO;
using System.Text;
using System.Xml;

using DotNet.Common.Utility;

namespace DotNet.Common.Serialization
{
    /// <summary>
    /// Xml序列化基类。
    /// </summary>
	public class SerializerBase : ISerializer
	{
		#region [ ToFile ]

		/// <summary>
		/// 把对象序列化为Xml文本后，保存到Xml文件中。
		/// </summary>
		/// <typeparam name="T">要转换成Xml文本对象的类型。</typeparam>
		/// <param name="obj">要转换成Xml文本对象。</param>
		/// <param name="fileName">Xml文件名。</param>
		/// <returns>如果对象成功保存到Xml文件中返加 <b>true</b>, 反之返回 <b>false</b>。</returns>
		public bool ToFile<T>(T obj, string fileName) where T : class, new()
		{
			return ToFile<T>(obj, fileName, Encoding.UTF8);
		}

		/// <summary>
		/// 把对象序列化为Xml文本后，保存到Xml文件中。
		/// </summary>
		/// <typeparam name="T">要转换成Xml文本对象的类型。</typeparam>
		/// <param name="obj">要转换成Xml文本对象。</param>
		/// <param name="fileName">Xml文件名。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>如果对象成功保存到Xml文件中返加 <b>true</b>, 反之返回 <b>false</b>。</returns>
		public bool ToFile<T>(T obj, string fileName, Type[] extraTypes) where T : class, new()
		{
			return ToFile<T>(obj, fileName, Encoding.UTF8, extraTypes);
		}

		/// <summary>
		/// 把对象序列化为Xml文本后，保存到Xml文件中。
		/// </summary>
		/// <typeparam name="T">要转换成Xml文本对象的类型。</typeparam>
		/// <param name="obj">要转换成Xml文本对象。</param>
		/// <param name="fileName">Xml文件名。</param>
		/// <param name="encoding">文件编码。</param>
		/// <returns>如果对象成功保存到Xml文件中返加 <b>true</b>, 反之返回 <b>false</b>。</returns>
		public bool ToFile<T>(T obj, string fileName, Encoding encoding) where T : class, new()
		{
			return ToFile<T>(obj, fileName, encoding, new Type[] { });
		}

		/// <summary>
		/// 把对象序列化为Xml文本后，保存到Xml文件中。
		/// </summary>
		/// <typeparam name="T">要转换成Xml文本对象的类型。</typeparam>
		/// <param name="obj">要转换成Xml文本对象。</param>
		/// <param name="fileName">Xml文件名。</param>
		/// <param name="encoding">文件编码。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>如果对象成功保存到Xml文件中返加 <b>true</b>, 反之返回 <b>false</b>。</returns>
		public virtual bool ToFile<T>(T obj, string fileName, Encoding encoding, Type[] extraTypes) where T : class, new()
		{
			bool saved = true;

			try
			{
				string serializedString = ToSerializedString(obj, encoding, extraTypes);
				if (!StringHelper.IsEmpty(serializedString))
				{
					XmlDocument doc = new XmlDocument();
					doc.LoadXml(serializedString);
					doc.Save(fileName);
				}

				saved = true;
			}
			catch
			{
				saved = false;
			}

			return saved;
		}

		#endregion

		#region [ ToSerializedString ]

		/// <summary>
		/// 将对象序列化为UTF-8格式的文本。 
		/// </summary>
		/// <param name="obj">要序列化为文本的对象。</param>
		/// <returns>
		/// 如果 <paramref name="obj"/> 是空引用，返回 <seealso cref="String.Empty"/>, 反之返回序列化文本。
		/// </returns>
		public string ToSerializedString(object obj)
        {
			return ToSerializedString(obj, Encoding.UTF8);
        }

		/// <summary>
		/// 将对象序列化为UTF-8格式的文本。 
		/// </summary>
		/// <param name="obj">要序列化为文本的对象。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>
		/// 如果 <paramref name="obj"/> 是空引用，返回 <seealso cref="String.Empty"/>, 反之返回序列化文本。
		/// </returns>
		public string ToSerializedString(object obj, Type[] extraTypes)
		{
			return ToSerializedString(obj, Encoding.UTF8, extraTypes);
		}

		/// <summary>
		/// 将对象序列化为指定编码格式的文本。 
		/// </summary>
		/// <param name="obj">要序列化为文本的对象。</param>
		/// <param name="encoding">文本编码格式。如果为空引用则默认为UTF-8编码格式。</param>
		/// <returns>
		/// 如果 <paramref name="obj"/> 是空引用，返回 <seealso cref="String.Empty"/>, 反之返回序列化文本。
		/// </returns>
		public string ToSerializedString(object obj, Encoding encoding)
        {
			return ToSerializedString(obj, encoding, new Type[] { });
		}

		/// <summary>
		/// 将对象序列化为指定编码格式的文本。 
		/// </summary>
		/// <param name="obj">要序列化为文本的对象。</param>
		/// <param name="encoding">文本编码格式。如果为空引用则默认为UTF-8编码格式。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>
		/// 如果 <paramref name="obj"/> 是空引用，返回 <seealso cref="String.Empty"/>, 反之返回序列化文本。
		/// </returns>
		public virtual string ToSerializedString(object obj, Encoding encoding, Type[] extraTypes)
		{
			byte[] bytes = ToBinary(obj, encoding, extraTypes);
			return encoding.GetString(bytes).TrimStart();
		}

		#endregion

		#region [ ToBinary ]

		/// <summary>
		/// 把对象转换成二进制数据。
		/// </summary>
		/// <param name="obj">要转换成二进制数据的对象。</param>
		/// <returns>二进制数据。</returns>
		public byte[] ToBinary(object obj)
        {
			return ToBinary(obj, Encoding.UTF8);
        }

		/// <summary>
		/// 把对象转换成二进制数据。
		/// </summary>
		/// <typeparam name="T">要转换成二进制数据对象的类型。</typeparam>
		/// <param name="obj">要转换成二进制数据的对象。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>二进制数据。</returns>
		public byte[] ToBinary(object obj, Type[] extraTypes)
		{
			return ToBinary(obj, Encoding.UTF8, extraTypes);
		}

		/// <summary>
		/// 把对象转换成二进制数据。
		/// </summary>
		/// <param name="obj">要转换成二进制数据的对象。</param>
		/// <param name="encoding">Xml文本编码。</param>
		/// <returns>二进制数据。</returns>
		public byte[] ToBinary(object obj, Encoding encoding)
		{
			return ToBinary(obj, encoding, new Type[] { });
		}

		/// <summary>
		/// 把对象转换成二进制数据。
		/// </summary>
		/// <typeparam name="T">要转换成二进制数据对象的类型。</typeparam>
		/// <param name="obj">要转换成二进制数据的对象。</param>
		/// <param name="encoding">二进制数据编码格式。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>二进制数据。</returns>
		public virtual byte[] ToBinary(object obj, Encoding encoding, Type[] extraTypes)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region [ FromFile ]

		/// <summary>
		/// 把xml文件反序列化成对象。
		/// </summary>
		/// <typeparam name="T">要转换成的对象类型。</typeparam>
		/// <param name="fileName">xml文件。</param>
		/// <returns>对象。</returns>
		/// <remarks>
		/// 默认编码为UTF8。
		/// </remarks>
		public T FromFile<T>(string fileName) where T : class, new()
		{
            return FromFile<T>(fileName, Encoding.UTF8);
		}

		/// <summary>
		/// 把xml文件反序列化成对象。
		/// </summary>
		/// <typeparam name="T">要转换成的对象类型。</typeparam>
		/// <param name="fileName">xml文件。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>对象。</returns>
		/// <remarks>
		/// 默认编码为UTF8。
		/// </remarks>
		public T FromFile<T>(string fileName, Type[] extraTypes) where T : class, new()
		{
			return FromFile<T>(fileName, Encoding.UTF8, extraTypes);
		}

        /// <summary>
        /// 把xml文件反序列化成对象。
        /// </summary>
        /// <typeparam name="T">要转换成的对象类型。</typeparam>
        /// <param name="fileName">xml文件。</param>
        /// <param name="encoding">文件编码。</param>
        /// <returns>对象。</returns>
        public T FromFile<T>(string fileName, Encoding encoding) where T : class, new()
        {
			return FromFile<T>(fileName, encoding, new Type[] { });
		}

		/// <summary>
		/// 把xml文件反序列化成对象。
		/// </summary>
		/// <typeparam name="T">要转换成的对象类型。</typeparam>
		/// <param name="fileName">xml文件。</param>
		/// <param name="encoding">文件编码格式。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>对象。</returns>
		public T FromFile<T>(string fileName, Encoding encoding, Type[] extraTypes) where T : class, new()
		{
            T ret = default(T);
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[0];
                BinaryReader r = new BinaryReader(fs, encoding);
                r.BaseStream.Seek(0, SeekOrigin.Begin);    //将文件指针设置到文件开
                bytes = r.ReadBytes((int)r.BaseStream.Length);

                ret = (T)FromBinary<T>(bytes, encoding, extraTypes);
            }

            return ret;
        }

		#endregion

		#region [ FromSerializedString ]

		/// <summary>
        /// 把xml文本反序列化成对象。
        /// </summary>
        /// <typeparam name="T">要转换成的对象类型。</typeparam>
        /// <param name="xml">xml文本</param>
        /// <returns>对象。</returns>
		public T FromSerializedString<T>(string serializedString) where T : class, new()
        {
			return FromSerializedString<T>(serializedString, Encoding.UTF8);
        }

		/// <summary>
		/// 把文本反序列化成对象。
		/// </summary>
		/// <typeparam name="T">要转换成的对象类型。</typeparam>
		/// <param name="serializedString">序列化的文本。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>对象。</returns>
		/// <remarks>
		/// 默认编码为UTF8。
		/// </remarks>
		public T FromSerializedString<T>(string serializedString, Type[] extraTypes) where T : class, new()
		{
			return FromSerializedString<T>(serializedString, Encoding.UTF8, extraTypes);
		}

		/// <summary>
		/// 把xml文本反序列化成对象。
		/// </summary>
		/// <typeparam name="T">要转换成的对象类型。</typeparam>
		/// <param name="serializedString">xml文本</param>
		/// <param name="encoding">文本编码。</param>
		/// <returns>对象。</returns>
		public T FromSerializedString<T>(string serializedString, Encoding encoding) where T : class, new()
		{
			return FromSerializedString<T>(serializedString, encoding, new Type[] { });
		}

		/// <summary>
		/// 把文本反序列化成对象。
		/// </summary>
		/// <typeparam name="T">要转换成的对象类型。</typeparam>
		/// <param name="serializedString">序列化的文本。</param>
		/// <param name="encoding">xml文本编码。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>对象。</returns>
		public virtual T FromSerializedString<T>(string serializedString, Encoding encoding, Type[] extraTypes) where T : class, new()
		{
			T ret = default(T);
			if (StringHelper.IsEmpty(serializedString))
			{
				return ret;
			}

			byte[] bytes = encoding.GetBytes(serializedString);

			return FromBinary<T>(bytes, encoding, extraTypes);
		}

		#endregion

		#region [ FromBinary ]

		/// <summary>
        /// 把二进制数据反序列化成对象。
        /// </summary>
        /// <typeparam name="T">要转换成的对象类型。</typeparam>
        /// <param name="bytes">二进制数据</param>
        /// <returns>对象。</returns>
        public T FromBinary<T>(byte[] bytes) where T : class, new()
        {
			return FromBinary<T>(bytes, Encoding.UTF8, new Type[] { });
        }

		/// <summary>
		/// 把二进制数据反序列化成对象。
		/// </summary>
		/// <typeparam name="T">要转换成的对象类型。</typeparam>
		/// <param name="bytes">二进制数据</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>对象。</returns>
		public T FromBinary<T>(byte[] bytes, Type[] extraTypes) where T : class, new()
		{
			return FromBinary<T>(bytes, Encoding.UTF8, extraTypes);
		}

        /// <summary>
        /// 把二进制数据反序列化成对象。
        /// </summary>
        /// <typeparam name="T">要转换成的对象类型。</typeparam>
        /// <param name="bytes">二进制数据</param>
        /// <param name="encoding">编码。</param>
        /// <returns>对象。</returns>
        public T FromBinary<T>(byte[] bytes, Encoding encoding) where T : class, new()
        {
			return FromBinary<T>(bytes, encoding, new Type[] { });
        }

		/// <summary>
		/// 把二进制数据反序列化成对象。
		/// </summary>
		/// <typeparam name="T">要转换成的对象类型。</typeparam>
		/// <param name="bytes">二进制数据</param>
		/// <param name="encoding">编码。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>对象。</returns>
		public T FromBinary<T>(byte[] bytes, Encoding encoding, Type[] extraTypes) where T : class, new()
		{
			return (T)FromBinary(typeof(T), bytes, encoding, extraTypes); 
		}

		/// <summary>
		/// 把二进制数据反序列化成对象。
		/// </summary>
		/// <param name="bytes">二进制数据</param>
		/// <returns>对象。</returns>
		public object FromBinary(byte[] bytes)
		{
			return FromBinary(null, bytes, Encoding.UTF8);
		}

		/// <summary>
		/// 把二进制数据反序列化成对象。
		/// </summary>
		/// <param name="bytes">二进制数据</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>对象。</returns>
		public virtual object FromBinary(byte[] bytes, Type[] extraTypes)
		{
			return FromBinary(null, bytes, Encoding.UTF8, extraTypes);
		}

		/// <summary>
		/// 把二进制数据反序列化成对象。
		/// </summary>
		/// <param name="type">要转换成的对象类型。</param>
		/// <param name="bytes">二进制数据</param>
		/// <returns>对象。</returns>
		public object FromBinary(Type type, byte[] bytes)
		{
            return FromBinary(type, bytes, Encoding.UTF8);
		}

		/// <summary>
		/// 把二进制数据反序列化成对象。
		/// </summary>
		/// <param name="type">要转换成的对象类型。</param>
		/// <param name="bytes">二进制数据</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>对象。</returns>
		public object FromBinary(Type type, byte[] bytes, Type[] extraTypes)
		{
			return FromBinary(type, bytes, Encoding.UTF8, extraTypes);
		}

        /// <summary>
        /// 把二进制数据反序列化成对象。
        /// </summary>
        /// <param name="type">要转换成的对象类型。</param>
        /// <param name="bytes">二进制数据</param>
        /// <param name="encoding">编码。</param>
        /// <returns>对象。</returns>
        public object FromBinary(Type type, byte[] bytes, Encoding encoding)
        {
			return FromBinary(type, bytes, encoding, new Type[] { });
		}

		/// <summary>
		/// 把二进制数据反序列化成对象。
		/// </summary>
		/// <param name="type">要转换成的对象类型。</param>
		/// <param name="bytes">二进制数据</param>
		/// <param name="encoding">编码。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>对象。</returns>
		public virtual object FromBinary(Type type, byte[] bytes, Encoding encoding, Type[] extraTypes)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}