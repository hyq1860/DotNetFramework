using System;
using System.Xml.Serialization;

namespace DotNet.Data.Configuration
{
    /// <summary>
    /// configuration that contains the list of DataCommand configuration files.
    /// This class is for internal use only.
    /// </summary>
    [Serializable]
    [XmlRoot( "dataCommandFiles" )]
    public class ConfigDataCommandFileList
    {
        private DataCommandFile[] commandFiles;

        [XmlElement( "file" )]
        public DataCommandFile[] FileList
        {
            get { return commandFiles; }
            set { commandFiles = value; }
        }


    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class DataCommandFile
    {
        private string fileName;

        [XmlAttribute( "name" )]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
    }
}
