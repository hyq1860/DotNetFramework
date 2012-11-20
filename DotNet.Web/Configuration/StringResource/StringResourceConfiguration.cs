using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DotNet.Common.Configuration;

namespace DotNet.Web.Configuration
{

    using StringResourceHashTable = Dictionary<string, string>;

    [ConfigFile("~/Language/zh-cn/StringResource.config", ConfigPathType.ServerPath)]
    [XmlRoot("webStringResources", Namespace = "http://www.YinTai.com/Language")]
    public class StringResourceConfiguration
    {
        private StringResourceHashTable m_StringResourceHashTable;
        private List<StringResource> m_StringResourceList;
        private object m_SyncObject = new object();

        /// <summary>
        /// StringResourceList
        /// </summary>
        [XmlElement("stringResource")]
        public List<StringResource> StringResourceList
        {
            get
            {
                return this.m_StringResourceList;
            }
            set
            {
                this.m_StringResourceList = value;
            }
        }

        /// <summary>
        /// Gets the text.
        /// Returns "" if there is no text exists.
        /// </summary>
        /// <param name="StringResourceType">The Text Alias</param>
        /// <returns>Text</returns>
        public string GetText(string stringResourceKey)
        {
            PrepareAliasHashtable();
            string strText;
            bool bSuccess = m_StringResourceHashTable.TryGetValue(stringResourceKey, out strText);
            if (bSuccess)
            {
                return strText;
            }
            else
            {
                return string.Empty;
            }
        }

        private void PrepareAliasHashtable()
        {
            if (m_StringResourceHashTable == null)
            {
                lock (m_SyncObject)
                {
                    if (m_StringResourceHashTable == null)
                    {
                        m_StringResourceHashTable = new StringResourceHashTable(StringResourceList.Count);
                        foreach (StringResource srInfo in StringResourceList)
                        {
                            if (!m_StringResourceHashTable.ContainsKey(srInfo.StringAlias))
                            {
                                m_StringResourceHashTable.Add(srInfo.StringAlias, srInfo.Text);
                            }
                        }
                    }
                }
            }
        }
    }

    public class StringResource
    {

        private string m_Key;

        private string m_Text;

        /// <remarks/>
        [XmlAttribute("key")]
        public string StringAlias
        {
            get
            {
                return this.m_Key;
            }
            set
            {
                this.m_Key = value;
            }
        }

        /// <remarks/>
        [XmlAttribute("text")]
        public string Text
        {
            get
            {
                return this.m_Text;
            }
            set
            {
                this.m_Text = value;
            }
        }
    }
}
