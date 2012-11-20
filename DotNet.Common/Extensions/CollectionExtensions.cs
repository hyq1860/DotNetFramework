using System.Collections.Generic;
using System.IO;

namespace DotNet.Common
{
    /// <summary>
    /// 集合的一些扩展方法
    /// </summary>
    public static class CollectionExtensions 
    {
        /// <summary>
        /// Saves the contents of a Dictionary collection to disk.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="path">Path of file to hold data</param>
        public static void Save(this Dictionary<string, string> dictionary, string path) 
        {
            using (StreamWriter writer = new StreamWriter(path)) 
            {
                foreach (var item in dictionary) 
                {
                    writer.WriteLine("{0}={1}", item.Key, item.Value);
                }
            }
        }

        /// <summary>
        /// Loads the contents of a Dictionary collection from disk.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="path">Path of file that holds data</param>
        public static void Load(this Dictionary<string, string> dictionary, string path) 
        {
            dictionary.Clear();
            using (StreamReader reader = new StreamReader(path)) 
            {
                string line = reader.ReadLine();
                while (line != null) 
                {
                    // Parse key/value
                    int i = line.IndexOf('=');
                    if (i >= 0) {
                        string key = line.Substring(0, i).Trim();
                        string val = line.Substring(i + 1);
                        if (key.Length > 0 && !dictionary.ContainsKey(key))
                            dictionary.Add(key, val);
                    }
                    line = reader.ReadLine();
                }
            }
        }

        #region ListExtensions
        public static void AddRangeIfNotNull<T>(this List<T> src, IEnumerable<T> range) 
        {
            if (range != null)
                src.AddRange(range);
        }
        #endregion

        #region
        public static TDictionary CopyFrom<TDictionary, TKey, TValue>(
        this TDictionary source,
        IDictionary<TKey, TValue> copy)
        where TDictionary : IDictionary<TKey, TValue> 
        {
            foreach (var pair in copy) 
            {
                source.Add(pair.Key, pair.Value);
            }

            return source;
        }

        public static TDictionary CopyFrom<TDictionary, TKey, TValue>(
            this TDictionary source,
            IDictionary<TKey, TValue> copy,
            IEnumerable<TKey> keys)
            where TDictionary : IDictionary<TKey, TValue> 
        {
            foreach (var key in keys) 
            {
                source.Add(key, copy[key]);
            }

            return source;
        }

        public static TDictionary RemoveKeys<TDictionary, TKey, TValue>(
            this TDictionary source,
            IEnumerable<TKey> keys)
            where TDictionary : IDictionary<TKey, TValue> 
        {
            foreach (var key in keys) 
            {
                source.Remove(key);
            }

            return source;
        }

        public static IDictionary<TKey, TValue> RemoveKeys<TKey, TValue>(
            this IDictionary<TKey, TValue> source,
            IEnumerable<TKey> keys) 
        {
            foreach (var key in keys) 
            {
                source.Remove(key);
            }
            return source;
        }
        #endregion

    }
}
