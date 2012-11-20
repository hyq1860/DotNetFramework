using System;
using System.Configuration;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// Represents a configuration section that can be serialized and deserialized to XML.
    /// </summary>
    public class SerializableConfigurationSection : ConfigurationSection, IXmlSerializable
    {
        /// <summary>
        /// Returns the XML schema for the configuration section.
        /// </summary>
        /// <returns>A string with the XML schema, or <see langword="null"/> (<b>Nothing</b> 
        /// in Visual Basic) if there is no schema.</returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Updates the configuration section with the values from an <see cref="XmlReader"/>.
        /// </summary>
        /// <param name="reader">The <see cref="XmlReader"/> that reads the configuration source located at the element that describes the configuration section.</param>
        public void ReadXml(XmlReader reader)
        {
            reader.Read();
            DeserializeSection(reader);
        }

        /// <summary>
        /// Writes the configuration section values as an XML element to an <see cref="XmlWriter"/>.
        /// </summary>
        /// <param name="writer">The <see cref="XmlWriter"/> that writes to the configuration source.</param>
        public void WriteXml(XmlWriter writer)
        {
            string serialized = SerializeSection(this, "SerializableConfigurationSection", ConfigurationSaveMode.Full);
            writer.WriteRaw(serialized);
        }
    }
}
