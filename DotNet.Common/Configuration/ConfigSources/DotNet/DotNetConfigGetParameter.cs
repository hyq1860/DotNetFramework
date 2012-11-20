namespace DotNet.Common.Configuration.DotNetConfig
{
    /// <summary>
    /// 
    /// </summary>
    public class DotNetConfigGetParameter : FileConfigGetParameter, IConfigParameter
    {
        private string _sectionName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public DotNetConfigGetParameter(string path, string sectionName)
            : base(path)
        {
            _sectionName = sectionName;
        }

        /// <summary>
        /// 获取配置结点名称。
        /// </summary>
        public string SectionName
        {
            get { return _sectionName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string Name
        {
            get
            {
                return string.Format("DotNetConfig_{0}_{1}_{2}", Path, Filter, SectionName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string GroupName
        {
            get
            {
                return string.Format("DotNetConfig_{0}_{1}", Path, Filter); 
            }
        }
    }
}