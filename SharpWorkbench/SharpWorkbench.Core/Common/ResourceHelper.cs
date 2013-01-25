// -----------------------------------------------------------------------
// <copyright file="ResourceHelper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Resources;

namespace SharpWorkbench.Core.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ResourceHelper
    {
        public static List<KeyValuePair<string,string>> GetResourceKeyValues(string resourcePath)
        {
            Assembly asm = typeof(ResourceHelper).Assembly;
            
            var data = new List<KeyValuePair<string, string>>();
            // Create a ResXResourceReader for the file items.resx.
            if (!File.Exists(resourcePath))
            {
                return data;
            }
            using (ResXResourceReader rsxr = new ResXResourceReader(resourcePath))
            {
                // Iterate through the resources and display the contents to the console.
                foreach (DictionaryEntry d in rsxr)
                {
                    data.Add(new KeyValuePair<string, string>(d.Key.ToString(), d.Value.ToString()));
                }
                //Close the reader.
                //rsxr.Close();
            }
            return data;
        }

        public static void Update(string resourcePath, string key, string value)
        {
            if (!File.Exists(resourcePath))
            {
                return;
            }
            using (ResXResourceWriter resourceWriter = new ResXResourceWriter(resourcePath))
            {
            }
        }

        public void Add(string resourcePath,string key,string value)
        {
            if (!File.Exists(resourcePath))
            {
                return;
            }
            // Create a resource writer.
            using (IResourceWriter rw = new ResourceWriter("myStrings.resources"))
            {
                // Add resources to the file.
                rw.AddResource(key, value);
            }
        }

        public void Add(string resourcePath, string key, object value)
        {
            // Create a resource writer.
            using (IResourceWriter rw = new ResourceWriter("app.resources"))
            {
                // Add resources to the file.
                rw.AddResource(key, value);
            }
        }

        public static void GenerateResources(string filePath)
        {
            // Create a resource writer.
            using (IResourceWriter rw = new ResourceWriter("app.resources"))
            {
                // Add resources to the file.
                var imageFiles = Directory.GetFiles(filePath);
                foreach (var imageFile in imageFiles)
                {
                    var image = System.Drawing.Image.FromFile(imageFile);
                    var fileInfo = new FileInfo(imageFile);
                    rw.AddResource(fileInfo.FullName, image);
                }
            }
        }
    }
}
