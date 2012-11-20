using System;
using System.Configuration;

namespace DotNet.Common.Utility
{
    public class CommonConfig
    {
        public static readonly CommonConfig Instance = new CommonConfig();
        private string m_UploadFileType;
        private int m_TotalImageLimit;
        private int m_ImageSizeLimit;

        private CommonConfig()
        {
            string limit = ConfigurationManager.AppSettings["TotalImageLimit"];
            if (string.IsNullOrEmpty(limit) == true)
            {
                this.m_TotalImageLimit = 10;
            }
            else
            {
                this.m_TotalImageLimit = Convert.ToInt32(limit);
            }

            limit = ConfigurationManager.AppSettings["ImageSizeLimit"];
            if (string.IsNullOrEmpty(limit) == true)
            {
                this.m_ImageSizeLimit = 40;
            }
            else
            {
                this.m_ImageSizeLimit = Convert.ToInt32(limit);
            }

            string uploadFileType = ConfigurationManager.AppSettings["UploadFileType"];
            if (string.IsNullOrEmpty(uploadFileType) == true)
            {
                throw new ApplicationException("Not set allow UploadFileType");
            }
            else
            {
                this.m_UploadFileType = uploadFileType;
            }
        }

        public int TotalImageLimit
        {
            get
            {
                return this.m_TotalImageLimit;
            }
        }

        public int ImageSizeLimit
        {
            get
            {
                return this.m_ImageSizeLimit;
            }
        }
        public string UploadFileType
        {
            get
            {
                return this.m_UploadFileType;
            }
        }
    }
}
