// -----------------------------------------------------------------------
// <copyright file="FileRead.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNetTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FileRead
    {
        public static List<string> Read(string key)
        {
            var files = new List<string>();
            var dirFiles = System.IO.Directory.GetFiles("d:\\data\\data");
            if (dirFiles.Any())
            {
                foreach (var dirFile in dirFiles)
                {
                    var txt= File.ReadAllText(dirFile);
                    if (txt.Contains(key))
                    {
                        files.Add(dirFile);
                    }
                }
            }
            return files;
        }

        public static void Read()
        {
            var dict = new Dictionary<string, string>();
            var txt=File.ReadAllText("d:\\Params.config");
            var index = 0;
            while ((index=txt.IndexOf("<params key=\"")) > 0)
            {
                var secondIndex = txt.IndexOf("\"",index);
                if (secondIndex > 0)
                {
                    var key = txt.Substring(index, secondIndex - index);
                    dict.Add(key,"");
                }
            }
        }
    }
}
