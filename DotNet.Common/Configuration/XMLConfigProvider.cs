using System;
using System.Collections.Generic;
using System.Text;
using DotNet.Common.Utility;
using System.IO;

namespace DotNet.Common.Configuration
{
    public class XMLConfigProvider : IConfigProvider
    {
        public T LoadConfiguration<T>(string configStoreInfor) where T : class
        {
            if (string.IsNullOrEmpty(configStoreInfor))
            {
                return null;
            }

            string storeFileName = GetCacheInfor(configStoreInfor);
            if (!File.Exists(storeFileName))
            {
                return null;
            }

            try
            {
                T resultLoad = ObjectXmlSerializer.LoadFromXml<T>(storeFileName);

                return resultLoad;
            }
            catch
            {
                return null;
            }
        }

        public string SaveConfiguration<T>(string configStoreInfor, T configInfor) where T : class
        {
            if (string.IsNullOrEmpty(configStoreInfor) || configInfor == null)
            {
                return "100001";
            }

            string storeFileName = GetCacheInfor(configStoreInfor);
            if (!File.Exists(storeFileName))
            {
                return "100003";
            }

            bool saveFlag = ObjectXmlSerializer.SaveXmlToFlie<T>(storeFileName, configInfor);
            if (saveFlag)
            {
                return string.Empty;
            }
            else
            {
                return "100004";
            }
        }

        public string GetCacheInfor(string configStoreInfor)
        {            
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configStoreInfor.Replace('/', '\\').TrimStart('\\'));
        }
    }
}
