using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 异常返回模式枚举
    /// Exception	:以异常形式向上抛出捕捉到的异常
    /// ErrorCode	:以错误代码的形式返回异常
    /// None		:不作任何返回
    /// </summary>
    public enum ExReturnMode { Exception = 1, ExceptionString, ErrorCode, ErrorString };

    /// <summary>
    /// 异常提示方式
    /// None	:不会去调用弹出异常信息处理方法
    /// WebUI	:调用JS弹出异常信息
    /// WinUI	:调用MessageBox弹出异常信息
    /// </summary>
    public enum ExAlertType { None = 1, WebUI, WinUI }

    /// <summary>
    /// 根据ErrorCode获取异常信息的方式
    /// XML，从XML文件获取
    /// DB，从数据库获取
    /// </summary>
    public enum ErrorCodeSource { XML = 1, DB = 2 }

    [ConfigFile("ExceptionConfig.config")]
    [XmlRoot("ExManager")]
    public class ExManagerConfig
    {
        [XmlAttribute("ErrorCodeSource")]
        public string ErrorCodeSource { get; set; }

        [XmlAttribute("ConnectionString")]
        public string ConnectionString { get; set; }

        [XmlAttribute("DataTable")]
        public string DataTable { get; set; }

        [XmlElement("ExHandler")]
        public ExHandlerCollection ExHandlerList { get; set; }
    }

    public class ExHandlerCollection : KeyedCollection<string, ExHandler>
    {
        protected override string GetKeyForItem(ExHandler item)
        {
            return item.Name;
        }
    }

    [XmlRoot("ExHandler")]
    public class ExHandler
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Type")]
        public string Type { get; set; }

        [XmlAttribute("ReturnMode")]
        public string ReturnMode { get; set; }

        [XmlAttribute("AlertType")]
        public string AlertType { get; set; }

        [XmlAttribute( "DirectUrlFormat" )]
        public string DirectUrlFormat { get; set; }

        [XmlElement("LogHandler")]
        public LogHandlerCollection LogHandlerList { get; set; }
    }

    public class LogHandlerCollection : KeyedCollection<string, LogHandler>
    {
        protected override string GetKeyForItem(LogHandler item)
        {
            return item.Type;
        }
    }

    [XmlRoot("LogHandler")]
    public class LogHandler
    {
        [XmlAttribute("Type")]
        public string Type { get; set; }

        [XmlAttribute("ConnectionString")]
        public string ConnectionString { get; set; }

        [XmlAttribute("DataTable")]
        public string DataTable { get; set; }
    }
}
