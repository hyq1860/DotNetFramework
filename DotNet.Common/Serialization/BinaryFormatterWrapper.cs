/********************************************************************************
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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using DotNet.Common.Utility;

namespace DotNet.Common.Serialization
{
	/// <summary>
	/// BinaryFormatter序列化。
    /// http://www.cnblogs.com/shouzheng/archive/2009/03/17/901866.html
	/// </summary>
	/// <remarks>
	/// 只支持UTF8编码格式的序列反序列化。
	/// </remarks>
    public class BinaryFormatterWrapper : SerializerBase
    {
		#region [ Fields ]

		private static ISerializer serializer = new BinaryFormatterWrapper();
		private BinaryFormatter formatter = new BinaryFormatter();

		#endregion

		#region [ Methods ]

		/// <summary>
		/// 当前实例。
		/// </summary>
		public static ISerializer GetInstance()
		{
			return serializer;
		}

		#region [ ToFile ]

		// <summary>
		/// 把对象序列化为文本后，保存到文件中。
		/// </summary>
		/// <typeparam name="T">要转换成Xml文本对象的类型。</typeparam>
		/// <param name="obj">要转换成Xml文本对象。</param>
		/// <param name="fileName">Xml文件名。</param>
		/// <param name="encoding">文件编码。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>如果对象成功保存到Xml文件中返加 <b>true</b>, 反之返回 <b>false</b>。</returns>
		public override bool ToFile<T>(T obj, string fileName, Encoding encoding, Type[] extraTypes)
		{
			bool saved = true;

			try
			{
				using (FileStream fs = File.Open(fileName, FileMode.Create))
				{
					byte[] info = ToBinary(obj, encoding);
					fs.Write(info, 0, info.Length);
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
		/// 将对象序列化为指定编码格式的Base64文本。 
		/// </summary>
		/// <param name="obj">要序列化为文本的对象。</param>
		/// <param name="encoding">文本编码格式。不起作用因 <seealso cref="BinaryFormatter"/> 只能使用UTF-8编码格式。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>
		/// 如果 <paramref name="obj"/> 是空引用，返回 <seealso cref="String.Empty"/>, 反之返回Base64格式的序列化文本。
		/// </returns>
        public override string ToSerializedString(object obj, Encoding encoding, Type[] extraTypes)
        {
            byte[] bytes = ToBinary(obj, encoding);
            return Convert.ToBase64String(bytes);
		}

		#endregion

		#region [ ToBinary ]

		/// <summary>
		/// 把对象转换成二进制数据。
		/// </summary>
		/// <typeparam name="T">要转换成二进制数据对象的类型。</typeparam>
		/// <param name="obj">要转换成二进制数据的对象。</param>
		/// <param name="encoding">编码(不起作用)。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>二进制数据。</returns>
		public override byte[] ToBinary(object obj, Encoding encoding, Type[] extraTypes)
		{
			if (obj == null)
			{
				return new byte[] { };
			}

			byte[] bytes;
			using (MemoryStream stream = new MemoryStream())
			{
                formatter.Serialize(stream, obj);
                stream.Seek(0, 0);
                bytes = stream.ToArray();
                stream.Flush();
			}

			return bytes;
		}

		#endregion

		#region [ FromSerializedString ]

		/// <summary>
		/// 把文本反序列化成对象。
		/// </summary>
		/// <typeparam name="T">要转换成的对象类型。</typeparam>
		/// <param name="serializedString">序列化的文本。</param>
		/// <param name="encoding">编码。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>对象。</returns>
		public override T FromSerializedString<T>(string serializedString, Encoding encoding, Type[] extraTypes)
        {
            T ret = default(T);
			if (StringHelper.IsEmpty(serializedString))
            {
                return ret;
            }

			byte[] bytes = Convert.FromBase64String(serializedString);

            return FromBinary<T>(bytes, encoding, extraTypes);
		}

		#endregion

		#region [ FromBinary ]

		/// <summary>
		/// 把二进制数据反序列化成对象。
		/// </summary>
		/// <param name="type">要转换成的对象类型。(没有使用)</param>
		/// <param name="bytes">二进制数据</param>
        /// <param name="encoding">编码。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>对象。</returns>
		public override object FromBinary(Type type, byte[] bytes, Encoding encoding, Type[] extraTypes)
		{
			object ret = null;
			if (bytes == null || bytes.Length <= 0)
			{
				return ret;
			}

            using (MemoryStream stream = new MemoryStream(bytes))
			{
				ret = formatter.Deserialize(stream);
			}

			return ret;
		}

		#endregion

		#endregion
	}
}