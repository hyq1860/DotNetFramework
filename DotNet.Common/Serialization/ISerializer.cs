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
using System.Text;

namespace DotNet.Common.Serialization
{
	/// <summary>
	/// 序列化器。
	/// </summary>
	public interface ISerializer
	{
		#region [ ToFile ]

		/// <summary>
		/// 把对象序列化为Xml文本后，保存到Xml文件中。
		/// </summary>
		/// <typeparam name="T">要转换成Xml文本对象的类型。</typeparam>
		/// <param name="obj">要转换成Xml文本对象。</param>
		/// <param name="fileName">Xml文件名。</param>
		/// <returns>如果对象成功保存到Xml文件中返加 <b>true</b>, 反之返回 <b>false</b>。</returns>
		bool ToFile<T>(T obj, string fileName) where T : class, new();

		/// <summary>
		/// 把对象序列化为Xml文本后，保存到Xml文件中。
		/// </summary>
		/// <typeparam name="T">要转换成Xml文本对象的类型。</typeparam>
		/// <param name="obj">要转换成Xml文本对象。</param>
		/// <param name="fileName">Xml文件名。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>如果对象成功保存到Xml文件中返加 <b>true</b>, 反之返回 <b>false</b>。</returns>
		bool ToFile<T>(T obj, string fileName, Type[] extraTypes) where T : class, new();

		/// <summary>
		/// 把对象序列化为Xml文本后，保存到Xml文件中。
		/// </summary>
		/// <typeparam name="T">要转换成Xml文本对象的类型。</typeparam>
		/// <param name="obj">要转换成Xml文本对象。</param>
		/// <param name="fileName">Xml文件名。</param>
		/// <param name="encoding">文件编码。</param>
		/// <returns>如果对象成功保存到Xml文件中返加 <b>true</b>, 反之返回 <b>false</b>。</returns>
		bool ToFile<T>(T obj, string fileName, Encoding encoding) where T : class, new();

		/// <summary>
		/// 把对象序列化为Xml文本后，保存到Xml文件中。
		/// </summary>
		/// <typeparam name="T">要转换成Xml文本对象的类型。</typeparam>
		/// <param name="obj">要转换成Xml文本对象。</param>
		/// <param name="fileName">Xml文件名。</param>
		/// <param name="encoding">文件编码。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>如果对象成功保存到Xml文件中返加 <b>true</b>, 反之返回 <b>false</b>。</returns>
		bool ToFile<T>(T obj, string fileName, Encoding encoding, Type[] extraTypes) where T : class, new();

		#endregion

		#region [ ToSerializedString ]

		/// <summary>
		/// 将对象序列化为UTF-8格式的文本。 
		/// </summary>
		/// <param name="obj">要序列化为文本的对象。</param>
		/// <returns>
		/// 如果 <paramref name="obj"/> 是空引用，返回 <seealso cref="String.Empty"/>, 反之返回序列化文本。
		/// </returns>
		string ToSerializedString(object obj);

		/// <summary>
		/// 将对象序列化为UTF-8格式的文本。 
		/// </summary>
		/// <param name="obj">要序列化为文本的对象。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>
		/// 如果 <paramref name="obj"/> 是空引用，返回 <seealso cref="String.Empty"/>, 反之返回序列化文本。
		/// </returns>
		string ToSerializedString(object obj, Type[] extraTypes);

		/// <summary>
		/// 将对象序列化为指定编码格式的文本。 
		/// </summary>
		/// <param name="obj">要序列化为文本的对象。</param>
		/// <param name="encoding">文本编码格式。如果为空引用则默认为UTF-8编码格式。</param>
		/// <returns>
		/// 如果 <paramref name="obj"/> 是空引用，返回 <seealso cref="String.Empty"/>, 反之返回序列化文本。
		/// </returns>
		string ToSerializedString(object obj, Encoding encoding);

		/// <summary>
		/// 将对象序列化为指定编码格式的文本。 
		/// </summary>
		/// <param name="obj">要序列化为文本的对象。</param>
		/// <param name="encoding">文本编码格式。如果为空引用则默认为UTF-8编码格式。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>
		/// 如果 <paramref name="obj"/> 是空引用，返回 <seealso cref="String.Empty"/>, 反之返回序列化文本。
		/// </returns>
		string ToSerializedString(object obj, Encoding encoding, Type[] extraTypes);

		#endregion

		#region [ ToBinary ]

		/// <summary>
        /// 把对象转换成二进制数据。
        /// </summary>
        /// <typeparam name="T">要转换成二进制数据对象的类型。</typeparam>
		/// <param name="obj">要转换成二进制数据的对象。</param>
        /// <returns>二进制数据。</returns>
		byte[] ToBinary(object obj);

		/// <summary>
		/// 把对象转换成二进制数据。
		/// </summary>
		/// <typeparam name="T">要转换成二进制数据对象的类型。</typeparam>
		/// <param name="obj">要转换成二进制数据的对象。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>二进制数据。</returns>
		byte[] ToBinary(object obj, Type[] extraTypes);

		/// <summary>
		/// 把对象转换成二进制数据。
		/// </summary>
		/// <typeparam name="T">要转换成二进制数据对象的类型。</typeparam>
		/// <param name="obj">要转换成二进制数据的对象。</param>
		/// <param name="encoding">二进制数据编码格式。</param>
		/// <returns>二进制数据。</returns>
		byte[] ToBinary(object obj, Encoding encoding);

		/// <summary>
		/// 把对象转换成二进制数据。
		/// </summary>
		/// <typeparam name="T">要转换成二进制数据对象的类型。</typeparam>
		/// <param name="obj">要转换成二进制数据的对象。</param>
		/// <param name="encoding">二进制数据编码格式。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>二进制数据。</returns>
		byte[] ToBinary(object obj, Encoding encoding, Type[] extraTypes);

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
		T FromFile<T>(string fileName) where T : class, new();

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
		T FromFile<T>(string fileName, Type[] extraTypes) where T : class, new();

		/// <summary>
		/// 把xml文件反序列化成对象。
		/// </summary>
		/// <typeparam name="T">要转换成的对象类型。</typeparam>
		/// <param name="fileName">xml文件。</param>
		/// <param name="encoding">文件编码格式。</param>
		/// <returns>对象。</returns>
        T FromFile<T>(string fileName, Encoding encoding) where T : class, new();

		/// <summary>
		/// 把xml文件反序列化成对象。
		/// </summary>
		/// <typeparam name="T">要转换成的对象类型。</typeparam>
		/// <param name="fileName">xml文件。</param>
		/// <param name="encoding">文件编码格式。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>对象。</returns>
		T FromFile<T>(string fileName, Encoding encoding, Type[] extraTypes) where T : class, new();

		#endregion

		#region [ FromSerializedString ]

		/// <summary>
        /// 把文本反序列化成对象。
        /// </summary>
        /// <typeparam name="T">要转换成的对象类型。</typeparam>
		/// <param name="serializedString">序列化的文本。</param>
        /// <returns>对象。</returns>
		/// <remarks>
		/// 默认编码为UTF8。
		/// </remarks>
		T FromSerializedString<T>(string serializedString) where T : class, new();

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
		T FromSerializedString<T>(string serializedString, Type[] extraTypes) where T : class, new();

		/// <summary>
		/// 把文本反序列化成对象。
		/// </summary>
		/// <typeparam name="T">要转换成的对象类型。</typeparam>
		/// <param name="serializedString">序列化的文本。</param>
		/// <param name="encoding">xml文本编码。</param>
		/// <returns>对象。</returns>
		T FromSerializedString<T>(string serializedString, Encoding encoding) where T : class, new();

		/// <summary>
		/// 把文本反序列化成对象。
		/// </summary>
		/// <typeparam name="T">要转换成的对象类型。</typeparam>
		/// <param name="serializedString">序列化的文本。</param>
		/// <param name="encoding">xml文本编码。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>对象。</returns>
		T FromSerializedString<T>(string serializedString, Encoding encoding, Type[] extraTypes) where T : class, new();

		#endregion

		#region [ FromBinary ]

		/// <summary>
        /// 把二进制数据反序列化成对象。
        /// </summary>
        /// <typeparam name="T">要转换成的对象类型。</typeparam>
        /// <param name="bytes">二进制数据</param>
        /// <returns>对象。</returns>
        T FromBinary<T>(byte[] bytes) where T : class, new();

		/// <summary>
		/// 把二进制数据反序列化成对象。
		/// </summary>
		/// <typeparam name="T">要转换成的对象类型。</typeparam>
		/// <param name="bytes">二进制数据</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>对象。</returns>
		T FromBinary<T>(byte[] bytes, Type[] extraTypes) where T : class, new();

        /// <summary>
        /// 把二进制数据反序列化成对象。
        /// </summary>
        /// <typeparam name="T">要转换成的对象类型。</typeparam>
        /// <param name="bytes">二进制数据</param>
        /// <param name="encoding">编码。</param>
        /// <returns>对象。</returns>
        T FromBinary<T>(byte[] bytes, Encoding encoding) where T : class, new();

		/// <summary>
		/// 把二进制数据反序列化成对象。
		/// </summary>
		/// <typeparam name="T">要转换成的对象类型。</typeparam>
		/// <param name="bytes">二进制数据</param>
		/// <param name="encoding">编码。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>对象。</returns>
		T FromBinary<T>(byte[] bytes, Encoding encoding, Type[] extraTypes) where T : class, new();

		/// <summary>
		/// 把二进制数据反序列化成对象。
		/// </summary>
		/// <param name="bytes">二进制数据</param>
		/// <returns>对象。</returns>
		object FromBinary(byte[] bytes);

		/// <summary>
		/// 把二进制数据反序列化成对象。
		/// </summary>
		/// <param name="bytes">二进制数据</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>对象。</returns>
		object FromBinary(byte[] bytes, Type[] extraTypes);

		/// <summary>
		/// 把二进制数据反序列化成对象。
		/// </summary>
		/// <param name="type">要转换成的对象类型。</param>
		/// <param name="bytes">二进制数据</param>
		/// <returns>对象。</returns>
		object FromBinary(Type type, byte[] bytes);

		/// <summary>
		/// 把二进制数据反序列化成对象。
		/// </summary>
		/// <param name="type">要转换成的对象类型。</param>
		/// <param name="bytes">二进制数据</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>对象。</returns>
		object FromBinary(Type type, byte[] bytes, Type[] extraTypes);

        /// <summary>
        /// 把二进制数据反序列化成对象。
        /// </summary>
        /// <param name="type">要转换成的对象类型。</param>
        /// <param name="bytes">二进制数据</param>
        /// <param name="encoding">编码。</param>
        /// <returns>对象。</returns>
        object FromBinary(Type type, byte[] bytes, Encoding encoding);

		/// <summary>
		/// 把二进制数据反序列化成对象。
		/// </summary>
		/// <param name="type">要转换成的对象类型。</param>
		/// <param name="bytes">二进制数据</param>
		/// <param name="encoding">编码。</param>
		/// <param name="extraTypes">要序列化的其他对象类型的 Type 数组。</param>
		/// <returns>对象。</returns>
		object FromBinary(Type type, byte[] bytes, Encoding encoding, Type[] extraTypes);

		#endregion
	}
}