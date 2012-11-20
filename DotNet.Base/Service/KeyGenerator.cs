using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DotNet.Base.Service;
using DotNet.Data;

namespace DotNet.Base
{
    /// <summary>
    /// 序号生成器：需要在数据库中建立一个表
    /// </summary>
    public class KeyGenerator
    {
        private KeyGenerator()
        {

        }

        private static KeyGenerator _instance;
        /// <summary>
        /// 键值池大小
        /// </summary>
        private const int PoolSize = 1;
        private static readonly object LockHelper = new object();
        //private static Dictionary<string, KeyValueInfo> KeyValueInfos = new Dictionary<string, KeyValueInfo>();
        private static SynchronisedDictionary<string, KeyValueInfo> KeyValueInfos=new SynchronisedDictionary<string, KeyValueInfo>();
        /// <summary>
        /// 
        /// </summary>
        public static KeyGenerator Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockHelper)
                    {
                        if (_instance == null)
                        {
                            _instance = new KeyGenerator();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 获取下一个键值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetNextValue(string key) 
        {
            KeyValueInfo keyValueInfo;
                
            if (KeyValueInfos.ContainsKey(key))
            {
                keyValueInfo = KeyValueInfos[key];
            }
            else
            {
                keyValueInfo = new KeyValueInfo(key, PoolSize);
                KeyValueInfos.Add(key, keyValueInfo);
            }
            return keyValueInfo.NextKey;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class KeyValueInfo
    {
        private string _key;
        private int _keyMax;
        private int _keyMin;
        private int _nextKey;
        private int _poolSize;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="poolSize"></param>
        public KeyValueInfo(string key, int poolSize)
        {
            _key = key;
            _poolSize = poolSize;
            RetrieveFromDb();
        }

        /// <summary>
        /// 最小键值
        /// </summary>
        public int KeyMin
        {
            get
            {
                return _keyMin;
            }
        }
        
        /// <summary>
        /// 最大键值
        /// </summary>
        public int KeyMax
        {
            get
            {
                return _keyMax;
            }
        }
        
        /// <summary>
        /// 下一个键值
        /// </summary>
        public int NextKey
        {
            get
            {
                if (_nextKey > _keyMax)
                {
                    RetrieveFromDb();
                }
                return _nextKey++;
            }
        }

        /// <summary>
        /// 从数据库提取键的当前值
        /// </summary>
        private void RetrieveFromDb()
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("RetrieveFromDb"))
            {
                cmd.SetParameterValue("@poolSize", _poolSize);
                cmd.SetParameterValue("@TableName", _key);
                var obj = cmd.ExecuteScalar();
                int keyFromDb = Convert.ToInt32(obj);
                _keyMin = keyFromDb - _poolSize + 1;
                _keyMax = keyFromDb;
                _nextKey = _keyMin;
            }
        }
    }
}
