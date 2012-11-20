/********************************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author: Allen Wang(Allen.G.Wang@newegg.com) 
 * Create Date: 5/7/2009
 * Description:
 *          
 * Revision History:
 *      Date         Author               Description
 * 
*********************************************************************************/
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace DotNet.Common.Serialization
{
	/// <summary>
	/// Xml序列化。
	/// </summary>
	public class XmlSerializerWrapper : SerializerBase
	{
		#region [ Fields ]

		private static ISerializer serializer = new XmlSerializerWrapper();

		#endregion

		#region [ Methods ]

		/// <summary>
		/// 当前实例。
		/// </summary>
		public static ISerializer GetInstance()
		{
			return serializer;
		}

		/// <summary>
		/// 把对象转换成二进制数据。
		/// </summary>
		/// <typeparam name="T">要转换成二进制数据对象的类型。</typeparam>
		/// <param name="obj">要转换成二进制数据的对象。</param>
		/// <param name="encoding">二进制数据编码格式。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>二进制数据。</returns>
		public override byte[] ToBinary(object obj, Encoding encoding, Type[] extraTypes)
		{
			byte[] bytes = null;

			if (obj != null)
			{
				XmlSerializer serializer = new XmlSerializer(obj.GetType(), extraTypes);

				using (MemoryStream ms = new MemoryStream())
				{
					using (XmlTextWriter xtw = new XmlTextWriter(ms, encoding))
					{
						serializer.Serialize(xtw, obj);
						ms.Position = 0;
						bytes = ms.ToArray();
					}
				}
			}
			else
			{
				bytes = new byte[] { };
			}

			return bytes;
		}

		/// <summary>
		/// 不支持此序列化格式。
		/// </summary>
		/// <param name="bytes">要转换成的对象类型。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>对象。</returns>
		public override object FromBinary(byte[] bytes, Type[] extraTypes)
		{
			throw new NotSupportedException();
		}

        /// <summary>
        /// 把二进制数据反序列化成对象。
        /// </summary>
		/// <param name="type">要转换成的对象类型。</param>
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
                XmlSerializer serializer = new XmlSerializer(type, extraTypes);
                ret = serializer.Deserialize(stream);
            }

            return ret;
		}

		#endregion
	}
}