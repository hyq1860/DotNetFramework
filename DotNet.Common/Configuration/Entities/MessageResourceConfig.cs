using System.Collections.Generic;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace DotNet.Common.Configuration
{
    using MessageResourceHashTable = Dictionary<string, string>;

    [ConfigFile("~/Language/zh-CN/MessageResource.config", ConfigPathType.ServerPath)]
    [XmlRoot("messageResources")]
    public class MessageResourceConfig
    {
        private MessageResourceHashTable m_MessageResourceHashTable;
        private object m_SyncObject = new object();

        [XmlElement("messageResource")]
        public MessageResourceCollection MessageList{get; set;}

        public string GetText(string messageResourceKey)
        {
            PrepareAliasHashtable();

            string strText;
            bool bSuccess = m_MessageResourceHashTable.TryGetValue(messageResourceKey, out strText);
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
            if (m_MessageResourceHashTable == null)
            {
                lock (m_SyncObject)
                {
                    if (m_MessageResourceHashTable == null)
                    {
                        m_MessageResourceHashTable = new MessageResourceHashTable(MessageList.Count);
                        foreach (MessageResource curInfo in MessageList)
                        {
                            if (!m_MessageResourceHashTable.ContainsKey(curInfo.Key))
                            {
                                m_MessageResourceHashTable.Add(curInfo.Key, curInfo.Text);
                            }
                        }
                    }
                }
            }
        }
    }

    public class MessageResourceCollection : KeyedCollection<string, MessageResource>
    {
        protected override string GetKeyForItem(MessageResource item)
        {
            return item.Key;
        }
    }

    [XmlRoot("messageResource")]
    public class MessageResource
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("text")]
        public string Text { get; set; }

        [XmlElement("description")]
        public DescriptionCollection DescriptionList { get; set; }
    }

    public class DescriptionCollection : KeyedCollection<string, MessageDescription>
    {
        protected override string GetKeyForItem(MessageDescription item)
        {
            return item.Text;
        }
    }

    [XmlRoot("description")]
    public class MessageDescription
    {
        [XmlAttribute("text")]
        public string Text { get; set; }
    }
}
