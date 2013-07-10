/*******************************************************************************
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
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using DotNet.Common.Collections;
using DotNet.Common.Diagnostics;
using DotNet.Common.Resources;

namespace DotNet.Common.Utility
{
	/// <summary>
	/// 
	/// </summary>
	public class XmlUtils
	{
        /// <summary>
        /// 不带命名空间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public string ToXml<T>(T instance)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            //Add an empty namespace and empty value
            ns.Add("", "");

            XmlWriterSettings settings = new XmlWriterSettings();
            // Remove the <?xml version="1.0" encoding="utf-8"?>
            settings.OmitXmlDeclaration = true;
            // settings.DoNotEscapeUriAttributes = false;
            settings.Indent = true;
            settings.Encoding = Encoding.UTF8;
            settings.NewLineOnAttributes = true;
            XmlWriter sr = null;
            try
            {
                XmlSerializer xr = new XmlSerializer(typeof(T));
                StringBuilder sb = new StringBuilder();

                sr = XmlWriter.Create(sb, settings);
                xr.Serialize(sr, instance, ns);

                return (sb.ToString());
            }
            catch (Exception ex)
            {
                //Logger.Log(String.Format("Serialization Fail. Error Message : {0} {1}{2}", ex.Message, Environment.NewLine, ex.StackTrace));
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

        /// <summary>
        /// XMLs to entity.
        /// </summary>
        /// <typeparam name="T">Xml對應的Entity type</typeparam>
        /// <param name="xml">The XML.</param>
        /// <returns>Xml對應的Entity</returns>
        public static T XmlToEntity<T>(string xml) where T : class
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                byte[] byteArray = Encoding.UTF8.GetBytes(xml);
                MemoryStream stream = new MemoryStream(byteArray);

                return serializer.Deserialize(stream) as T;
            }
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="files"></param>
		/// <param name="fileContentMergeFail"></param>
		/// <returns></returns>
		public static FileMergeResult MergeFiles(List<string> files)
		{
			Check.Argument.IsNotNull("files", files);
			Check.Argument.IsTrue(files.Count > 0, "files", R.FileArrayEmpty);

			FileMergeResult mergeResult = new FileMergeResult(files);
			XmlDocument doc = new XmlDocument();
			if (files.Count > 0)
			{
				string fileToMerge = string.Empty;
				for (int i = 0; i < files.Count; i++)
				{
					fileToMerge = files[i];
					try
					{
						if (!File.Exists(fileToMerge))
						{
							ThrowHelper.ThrowFileNotFoundException(R.FileNotFound, fileToMerge);
						}

						if (doc.DocumentElement == null)
						{
							doc.Load(fileToMerge);
						}
						else
						{
							XmlDocument docTemp = new XmlDocument();
							docTemp.Load(fileToMerge);
							foreach (XmlNode node in docTemp.DocumentElement.ChildNodes)
							{
								XmlNode nodeTemp = doc.ImportNode(node, true);
								doc.DocumentElement.AppendChild(nodeTemp);
							}
						}

						mergeResult.FilesMerged.Add(fileToMerge);
					}
					catch (FileNotFoundException ex)
					{
						mergeResult.AddFileMergeFailReason(fileToMerge, ex);
					}
					catch (XmlException ex)
					{
						mergeResult.AddFileMergeFailReason(fileToMerge, string.Format(R.FileLoadFail_Xml, fileToMerge), ex);
					}
					catch (FileLoadException ex)
					{
						mergeResult.AddFileMergeFailReason(fileToMerge, string.Format(R.FileLoadFail_Xml, fileToMerge), ex);
					}
					catch (Exception ex)
					{
						throw ex;
					}
				}
			}

			mergeResult.FileContentMerged = doc.OuterXml;

			return mergeResult;
		}

		/// <summary>
		/// 从DataSet Xml 配置文件中获取 DataTable.
		/// </summary>
		/// <param name="file">完全限定路径配置文件。</param>
		/// <returns>DataTable数据集。</returns>
		public static DataTable LoadXmlDataTable(string file)
		{
			// if file does not exist, no log
			if (!File.Exists(file))
			{
				return null;
			}

			try
			{
				DataSet ds = new DataSet();
				XmlReadMode mode = ds.ReadXml(file, XmlReadMode.ReadSchema);
				return ds.Tables[0];
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		/// 从DataSet Xml 配置文件中获取 DataTable.
		/// </summary>
		/// <param name="files"></param>
		/// <returns></returns>
		public static DataTable LoadXmlDataTable(string[] files)
		{
			if (files == null || files.Length == 0)
			{
				return null;
			}

			DataTable dt = new DataTable();
			foreach (string file in files)
			{
				DataSet ds = new DataSet();
				ds.ReadXml(file, XmlReadMode.ReadSchema);
				if (ds.Tables.Count > 0)
				{
					dt.Merge(ds.Tables[0]);
				}
			}

			return dt;
		}
	}

	/// <summary>
	/// 文件合并结果。
	/// </summary>
	public class FileMergeResult
	{
		#region [ Fields ]

		private List<string> filesToMerge = new List<string>();
		private List<string> filesMerged = new List<string>();
		private string fileContentMerged = string.Empty;
		private KeyedCollection<FileMergeFailReason> fileMergeFailReasons =
			new KeyedCollection<FileMergeFailReason>();

		#endregion

		#region [ Constructors ]

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filesToMerge"></param>
		public FileMergeResult(List<string> filesToMerge)
		{
			this.filesToMerge = filesToMerge;
		}

		#endregion

		#region [ Properties ]

		/// <summary>
		/// 存在文件被合并。
		/// </summary>
		public bool HasFileMerged
		{
			get { return filesMerged.Count > 0; }
		}

		/// <summary>
		/// 所有文件都已经合并。
		/// </summary>
		public bool AllFilesMerged
		{
			get { return filesToMerge.Count == filesMerged.Count; }
		}

		/// <summary>
		/// 要合并的文件。
		/// </summary>
		public List<string> FilesToMerge
		{
			get { return this.filesToMerge; }
		}

		/// <summary>
		/// 正常合并的文件。
		/// </summary>
		public List<string> FilesMerged
		{
			get { return this.filesMerged; }
		}

		/// <summary>
		/// 合并的文件内容。
		/// </summary>
		public string FileContentMerged
		{
			get { return this.fileContentMerged; }
			set { this.fileContentMerged = value; }
		}

		/// <summary>
		/// 合并失败的文件及原因。
		/// </summary>
		public KeyedCollection<FileMergeFailReason> FileMergeFailReasons
		{
			get { return this.fileMergeFailReasons; }
		}

		#endregion

		#region [ Methods ]

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="mergeFailReason"></param>
		public void AddFileMergeFailReason(string fileName, string mergeFailReason)
		{
			AddFileMergeFailReason(fileName, mergeFailReason, null);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="mergeFailReason"></param>
		public void AddFileMergeFailReason(string fileName, Exception innerException)
		{
			Check.Argument.IsNotNull("innerException", innerException);

			AddFileMergeFailReason(fileName, innerException.Message, innerException);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="mergeFailReason"></param>
		public void AddFileMergeFailReason(string fileName, string mergeFailReason, Exception innerException)
		{
			FileMergeFailReason failReason = new FileMergeFailReason();
			failReason.FileName = fileName;
			failReason.MergeFailReason = mergeFailReason;
			failReason.InnerException = innerException;

			this.fileMergeFailReasons.Add(failReason);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (FileMergeFailReason failReason in this.fileMergeFailReasons)
			{
				if (sb.Length > 0)
				{
					sb.Append(",");
				}

				sb.AppendLine(string.Format("{0}:{1}", failReason.FileName, failReason.MergeFailReason));
			}

			return base.ToString();
		}

		#endregion
	}

	/// <summary>
	/// 文件合并失败原因。
	/// </summary>
	public class FileMergeFailReason : IKeyedObject
	{
		#region [ Fields ]

		private string fileName;
		private string mergeFailReason;
		private Exception innerException;

		#endregion

		#region [ Properties ]

		/// <summary>
		/// 键值。
		/// </summary>
		public string Key
		{
			get { return this.fileName; }
		}

		/// <summary>
		/// 合并失败的文件名。
		/// </summary>
		public string FileName
		{
			get { return this.fileName; }
			set { this.fileName = value; }
		}

		/// <summary>
		/// 合并失败原因。
		/// </summary>
		public string MergeFailReason
		{
			get { return this.mergeFailReason; }
			set { this.mergeFailReason = value; }
		}

		/// <summary>
		/// 内部异常。
		/// </summary>
		public Exception InnerException
		{
			get { return this.innerException; }
			set { this.innerException = value; }
		}

		#endregion
	}
}