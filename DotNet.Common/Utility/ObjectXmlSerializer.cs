
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using DotNet.Common.Core;
using DotNet.Common.Logging;

namespace DotNet.Common.Utility
{
    public static class ObjectXmlSerializer
    {
        public static string LogCategoryName = "Framework.ObjectXmlSerializer";

        /// <summary>
        /// deserialize an object from a file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T LoadFromXml<T>(string fileName) where T : class
        {
            FileStream fs = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                return (T)serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                Logger.Log(String.Format("Deserialization Fail. File : {0}, Error Message : {1}", fileName, ex.Message));
                return null;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// deserialize an object from a file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object LoadFromXml(string fileName, Type type)
        {
            FileStream fs = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(type);
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                return serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                Logger.Log(String.Format("Deserialization Fail. File : {0}, Error Message : {1}", fileName, ex.Message));
                return null;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// deserialize an object from a XML Message
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlMessage"></param>
        /// <returns>
        ///     Null is returned if any error occurs.
        /// </returns>
        public static T LoadFromXmlMessage<T>(string xmlMessage) where T : class
        {
            StringReader sReader = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                sReader = new StringReader(xmlMessage);
                return (T)serializer.Deserialize(sReader);
            }
            catch (Exception ex)
            {
                Logger.Log(String.Format("Deserialization Fail. XmlMessage: {0} , Error Message : {1}", xmlMessage, ex.Message));
                return null;
            }
            finally
            {
                if (sReader != null)
                {
                    sReader.Dispose();
                }
            }
        }

        /// <summary>
        /// serialize an object to a string value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns>
        ///  Null is returned if any error occurs.
        /// </returns>
        public static string ToStringXmlMessage<T>(T instance)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            // Remove the <?xml version="1.0" encoding="utf-8"?>
            settings.OmitXmlDeclaration = true;
            
            Utf8StringWriter sr = null;
            try
            {
                XmlSerializer xr = new XmlSerializer(typeof(T));
                StringBuilder sb = new StringBuilder();

                sr = new Utf8StringWriter(sb);
                xr.Serialize(sr, instance);

                return (sb.ToString());
            }
            catch (Exception ex)
            {
                Logger.Log(String.Format("Serialization Fail. Error Message : {0} {1}{2}", ex.Message, Environment.NewLine, ex.StackTrace));
                return null;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }

        public static bool SaveXmlToFlie<T>(string fileName, T xmlInfor) where T : class
        {
            try
            {
                string xmlStrInfor = ToStringXmlMessage<T>(xmlInfor);

                if (!string.IsNullOrEmpty(xmlStrInfor))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlStrInfor);
                    xmlDoc.Save(fileName);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(String.Format("Save Fail. Error Message : {0}", ex.Message));
                return false;
            }
        }

        /// <summary>
        /// 从字符串反序列化
        /// </summary>
        /// <typeparam name="T">要反序列化出的类型</typeparam>
        /// <param name="SerializerStr">要序列化的串</param>
        /// <param name="rootElementName">根节点元素名称</param>
        /// <returns>返回序列化出的对象</returns>
        public static T Deserializer<T>(string serializerStr, string rootElementName)
        {
            XmlRootAttribute xmlRootAttribute = new XmlRootAttribute(rootElementName);
            //xmlRootAttribute.Namespace = ExXmlDocument.NameSpace;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), xmlRootAttribute);
            StringReader sr = new StringReader(serializerStr);

            XmlReader xmlReader = XmlReader.Create(sr);
            return (T)xmlSerializer.Deserialize(xmlReader);
        }

        /// <summary>
        /// 从字符串反序列化
        /// </summary>
        /// <typeparam name="T">要反序列化出的类型</typeparam>
        /// <param name="SerializerStr">要序列化的串</param>        
        /// <returns>返回序列化出的对象</returns>
        public static T Deserializer<T>(string serializerStr)
        {
            return Deserializer<T>(serializerStr, "");
        }

        /// <summary>
        /// 从文件反序列化
        /// </summary>
        /// <typeparam name="T">要反序列化出的类型</typeparam>
        /// <param name="file">要序列化的文件</param>
        /// <param name="rootElementName">根节点元素名称</param>
        /// <returns>返回序列化出的对象</returns>
        public static T DeserializerFromFile<T>(string file, string rootElementName)
        {
            //
            //XmlReader xmlTextReader = XmlReader.Create(file);
            XmlTextReader xmlTextReader = new XmlTextReader(file);
            //Console.WriteLine(xmlTextReader.Encoding);
            //XmlReader xmlReader = XmlReader.Create(file);
            T result = (T)Deserializer<T>(xmlTextReader, rootElementName);
            xmlTextReader.Close();
            return result;
        }

        /// <summary>
        /// 从文件反序列化
        /// </summary>
        /// <typeparam name="T">要反序列化出的类型</typeparam>
        /// <param name="file">要序列化的文件</param>
        /// <param name="rootElementName">根节点元素名称</param>
        /// <returns>返回序列化出的对象</returns>
        public static T DeserializerFromFile<T>(string file)
        {
            return (T)DeserializerFromFile<T>(file, "");
        }

        /// <summary>
        /// 从XmlReader反序列化
        /// </summary>
        /// <typeparam name="T">要反序列化出的类型</typeparam>
        /// <param name="SerializerStr">要反序列化的XmlReader对象</param>
        /// <param name="rootElementName">根节点元素名称</param>
        /// <returns>返回序列化出的对象</returns>
        public static T Deserializer<T>(XmlReader xmlReader, string rootElementName)
        {
            XmlRootAttribute xmlRootAttribute = new XmlRootAttribute(rootElementName);
            //xmlRootAttribute.Namespace = ExXmlDocument.NameSpace;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), xmlRootAttribute);
            return (T)xmlSerializer.Deserialize(xmlReader);
        }

        /// <summary>
        /// 从XmlReader反序列化
        /// </summary>
        /// <typeparam name="T">要反序列化出的类型</typeparam>
        /// <param name="SerializerStr">要反序列化的XmlReader对象</param>
        /// <param name="rootElementName">根节点元素名称</param>
        /// <returns>返回序列化出的对象</returns>
        public static T Deserializer<T>(XmlReader xmlReader)
        {
            return Deserializer<T>(xmlReader, "");
        }

        /// <summary>
        /// 序列化到字符串
        /// </summary>
        /// <typeparam name="T">要序列化的类型</typeparam>
        /// <param name="serializerObj">要序列化的对象</param>
        /// <param name="rootElementName">根节点的名称</param>
        /// <returns>序列化的串</returns>
        public static string Serializer<T>(T serializerObj, string rootElementName)
        {
            //根节点属性
            XmlRootAttribute xmlRootAttribute = new XmlRootAttribute(rootElementName);
           // xmlRootAttribute.Namespace = ExXmlDocument.NameSpace;

            // 命名空间 
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            //xmlSerializerNamespaces.Add("", CommonLibrary.XML.ExXmlDocument.NameSpace);

            //建立序列化对象并序列化
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), xmlRootAttribute);

            //内存流方式
            MemoryStream memoryStream = new MemoryStream();
            XmlTextWriter xmlSink = new XmlTextWriter(memoryStream, Encoding.UTF8);
            xmlSerializer.Serialize(xmlSink, serializerObj, xmlSerializerNamespaces);
            //内在流reader转码方式
            memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
            System.IO.StreamReader reader = new System.IO.StreamReader(memoryStream, Encoding.UTF8);
            return reader.ReadToEnd();
            //内存流直接转码方式
            //byte[] utf8EncodedData = memoryStream.GetBuffer();
            //return System.Text.Encoding.UTF8.GetString(utf8EncodedData);


            ////旧序列化 经测试设置Encoding无效 一直默认为UTF16 此设置只对stream输出有效
            //StringBuilder sb = new StringBuilder();
            //XmlWriterSettings settings = new XmlWriterSettings();
            //settings.Encoding = Encoding.UTF8;
            //settings.Indent = true;
            //XmlWriter xw = XmlWriter.Create(sb, settings);

            //xmlSerializer.Serialize(xw, serializerObj, xmlSerializerNamespaces);

            //return sb.ToString();            
        }

        /// <summary>
        /// 序列化到字符串
        /// </summary>
        /// <typeparam name="T">要序列化的类型</typeparam>
        /// <param name="serializerObj">要序列化的对象</param>        
        /// <returns>序列化的串</returns>
        public static string Serializer<T>(T serializerObj)
        {
            return Serializer<T>(serializerObj, "");
        }

        /// <summary>
        /// 序列化到XmlWriter
        /// </summary>
        /// <typeparam name="T">要序列化的类型</typeparam>
        /// <param name="serializerObj">要序列化的对象</param>
        /// <param name="rootElementName">根元素的名称</param>
        /// <param name="xmlWriter">要写入序列化内容的对象 注意Encoding</param>
        public static void Serializer<T>(T serializerObj, string rootElementName, XmlWriter xmlWriter)
        {
            //根节点属性
            XmlRootAttribute xmlRootAttribute = new XmlRootAttribute(rootElementName);
            //xmlRootAttribute.Namespace = ExXmlDocument.NameSpace;

            // 命名空间 
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            //xmlSerializerNamespaces.Add("", CommonLibrary.XML.ExXmlDocument.NameSpace);

            //建立序列化对象并序列化            
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), xmlRootAttribute);
            xmlSerializer.Serialize(xmlWriter, serializerObj, xmlSerializerNamespaces);
        }

        /// <summary>
        /// 从XmlWriter序列化
        /// </summary>
        /// <typeparam name="T">要序列化的类型</typeparam>
        /// <param name="serializerObj">要序列化的对象</param>        
        /// <param name="xmlWriter">要写入序列化内容的对象</param>
        public static void Serializer<T>(T serializerObj, XmlWriter xmlWriter)
        {
            Serializer<T>(serializerObj, "", xmlWriter);
        }

        /// <summary>
        /// 序列化到文件
        /// </summary>
        /// <typeparam name="T">要序列化的类型</typeparam>
        /// <param name="serializerObj">要序列化的对象</param>
        /// <param name="rootElementName">根元素的名称</param>
        /// <param name="file">要写入的文件</param>
        public static void SerializerToFile<T>(T serializerObj, string rootElementName, string file)
        {
            //用XML Writer
            //XmlWriterSettings xws = new XmlWriterSettings();
            //xws.Encoding = System.Text.Encoding.UTF8;            
            //XmlWriter xmlWriter = XmlWriter.Create(file, xws);

            //用XMLTextWriter
            XmlTextWriter xmlTextWriter = new XmlTextWriter(file, System.Text.Encoding.UTF8);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.Indentation = 1;

            Serializer<T>(serializerObj, rootElementName, xmlTextWriter);
            xmlTextWriter.Close();
        }

        /// <summary>
        /// 序列化到文件
        /// </summary>
        /// <typeparam name="T">要序列化的类型</typeparam>
        /// <param name="serializerObj">要序列化的对象</param>        
        /// <param name="file">要写入的文件</param>
        public static void SerializerToFile<T>(T serializerObj, string file)
        {
            SerializerToFile<T>(serializerObj, "", file);
        }


        /// <summary>
        /// 从对象转化为xml字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeXml(object obj)
        {
            string xml = string.Empty;
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Encoding = Encoding.UTF8,
                OmitXmlDeclaration = true
            };
            //如果用StringBuilder，转化出来的xml 编码是UTF16，XmlWriterSettings不起作用，. 只能对Stream进行编码更改。
            MemoryStream stream = new MemoryStream();
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                xs.Serialize(writer, obj);
            }
            return Encoding.UTF8.GetString(stream.ToArray()).Trim();//此处转化为xml字符串后，会有一个编码是65279的小空格，大约只占1个像素.
        }

        /// <summary>
        /// 对象序列化为xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="isContainXmlHeader"></param>
        /// <returns></returns>
        public static string ToXml<T>(this T instance,string root,bool isContainXmlHeader,bool isContainDefaultNamespaces)
        {
            XmlSerializer xs = null;
            if (string.IsNullOrEmpty(root))
            {
                xs = new XmlSerializer(typeof(T));
            }
            else
            {
                xs = new XmlSerializer(typeof(T), new XmlRootAttribute(root));
            }

            XmlWriterSettings settings = new XmlWriterSettings()
                                             {
                                                 Encoding = Encoding.UTF8,
                                                 OmitXmlDeclaration = isContainXmlHeader
                                             };
            StringBuilder sb = new StringBuilder();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);
            using (Utf8StringWriter sw = new Utf8StringWriter(sb))
            {
                using (XmlWriter writer = XmlWriter.Create(sw, settings))
                {
                    if (isContainDefaultNamespaces)
                    {
                        xs.Serialize(writer, instance);
                    }
                    else
                    {
                        xs.Serialize(writer, instance, ns);
                    }
                    return sb.ToString();
                }
            }
        }
    }
}
