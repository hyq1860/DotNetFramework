using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Configuration;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Web;

namespace DotNet.Common.Utility
{
	public static class CommonUtily
	{
        //public static bool IsNull(this object @object)
        //{
        //    return (@object == null);
        //}

        //public static bool IsNotNull(this object @object)
        //{
        //    return (@object != null);
        //}

        //public static string ToJSON(this object @object)
        //{
        //    return JsonHelper.ObjectToJson(@object);
        //}

		public static bool CheckFileSize(long size)
		{
			long limit = (long)CommonConfig.Instance.ImageSizeLimit;

			if (size > limit * 1000)
			{
				return false;
			}

			return true;
		}

		public static bool CheckFileName(string fileName)
		{
			string fileType = CommonConfig.Instance.UploadFileType.Replace(".", "\\.");
			string reg = @"^http:\/\/(.*)(" + fileType + ")$";
			Regex rg = new Regex(reg, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			if (rg.IsMatch(fileName) != true)
			{
				return false;
			}

			return true;
		}

		public static byte[] ToByteArray(Stream stream)
		{
			Image imageFile = Image.FromStream(stream);

			using (MemoryStream myMS = new MemoryStream())
			{
				BinaryFormatter myBF = new BinaryFormatter();
				myBF.Serialize(myMS, imageFile);

				return myMS.ToArray();
			}

		}

		public static Image ToImage(byte[] imageBytes)
		{
			using (MemoryStream myMS = new MemoryStream(imageBytes, 0, imageBytes.Length))
			{
				BinaryFormatter myBF = new BinaryFormatter();
				object myObj = myBF.Deserialize(myMS);
				myMS.Close();

				return (Image)myObj;
			}
		}

		public static byte[] ToByteArray(Image imageFile)
		{
			using (MemoryStream myMS = new MemoryStream())
			{
				BinaryFormatter myBF = new BinaryFormatter();
				myBF.Serialize(myMS, imageFile);

				return myMS.ToArray();
			}

		}

        //public static HttpWebResponse GetWebImageStream(string url)
        //{
        //    if (string.IsNullOrEmpty(url)) return null;

        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    //request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322)";
        //    request.AllowAutoRedirect = true;

        //    string isProxy = ConfigurationManager.AppSettings["IsProxy"];
        //    if (isProxy == "true")
        //    {
        //        string userName = ConfigurationManager.AppSettings["ProxyName"];
        //        string userPwd = ConfigurationManager.AppSettings["ProxyPwd"];
        //        string domain = ConfigurationManager.AppSettings["ProxyDomain"];

        //        request.Proxy = new WebProxy(ConfigurationManager.AppSettings["ProxyAddress"]);
        //        request.Proxy.Credentials = new NetworkCredential(userName, userPwd, domain);
        //    }

        //    request.Timeout = 20000;
        //    request.Credentials = CredentialCache.DefaultCredentials;
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        //    return response;
        //}

		public static string GetAllIPAddress()
		{
			string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

			if (string.IsNullOrEmpty(ip))
			{
				ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
			}

			return ip;
		}

		public static string GetIPAddress()
		{
			string ip = GetAllIPAddress();

			if (!string.IsNullOrEmpty(ip))
			{
				ip = ip.Split(',')[0];
			}
			return ip;
		}
	}
}
