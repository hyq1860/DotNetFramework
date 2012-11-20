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

namespace DotNet.Common.Serialization
{
    public class Serializer
    {
        /// <summary>
        /// Xml 序列化。
        /// </summary>
        public static ISerializer XmlSerializer
        {
            get { return GetSerializer(SerializationMode.XmlSerializer); }
        }

        /// <summary>
        /// DataContract 序列化。
        /// </summary>
        public static ISerializer DataContractSerializer
        {
            get { return GetSerializer(SerializationMode.DataContractSerializer); }
        }

        /// <summary>
        /// NetDataContract 序列化。
        /// </summary>
        public static ISerializer NetDataContractSerializer
        {
            get { return GetSerializer(SerializationMode.NetDataContractSerializer); }
        }

        /// <summary>
        /// 二进制序列化。
        /// </summary>
        public static ISerializer BinaryFormatter
        {
            get { return GetSerializer(SerializationMode.BinaryFormatter); }
        }

        /// <summary>
        /// 获取序列化器。
        /// </summary>
        /// <param name="mode">序列化器的模式。</param>
        /// <returns>序列化器。</returns>
        public static ISerializer GetSerializer(SerializationMode mode)
        {
            ISerializer serializer = null;
            switch (mode)
            {
                case SerializationMode.XmlSerializer:
                    serializer = XmlSerializerWrapper.GetInstance();
                    break;
                //case SerializationMode.DataContractSerializer:
                //    serializer = DataContractSerializerWrapper.GetInstance();
                //    break;
                //case SerializationMode.NetDataContractSerializer:
                //    serializer = NetDataContractSerializerWrapper.GetInstance();
                    //break;
                case SerializationMode.BinaryFormatter:
                    serializer = BinaryFormatterWrapper.GetInstance();
                    break;
                default:
                    throw new NotSupportedException();
            }

            return serializer;
        }
    }
}