// -----------------------------------------------------------------------
// <copyright file="MongoDBOfficial.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using MongoDB.Driver;

namespace DotNet.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 官方版本的封装
    /// </summary>
    public class MongoDBOfficial : IDisposable
    {
        private MongoClient mongoClient;

        private MongoServer mongoServer;

        private MongoDatabase mongoDatabase;

        public MongoDBOfficial(string connectionString,string dataBaseName)
        {
            mongoClient = new MongoClient(connectionString);
            mongoServer = mongoClient.GetServer();
            mongoDatabase = mongoServer.GetDatabase(dataBaseName);

        }

        /// <summary>
        /// 获取当前连接数据库的指定集合【根据指定名称】
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">集合名称</param>
        /// <returns></returns>
        public MongoCollection<T> GetCollection<T>(string name) where T : class
        {
            return mongoDatabase.GetCollection<T>(name);
        }



        public void Dispose()
        {
            if (mongoServer != null)
            {
                mongoServer.Disconnect();
            }
            mongoClient = null;
            mongoServer = null;
        }
    }
}
