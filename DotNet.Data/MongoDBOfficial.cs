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
    /// http://www.mongodb.org/display/DOCS/CSharp+Language+Center
    /// http://www.mongodb.org/display/DOCS/CSharp+Driver+Tutorial
    /// www.mongodb.org/display/DOCS/CSharp+Driver+Serialization+Tutorial
    /// </summary>
    public class MongoDBOfficial : IDisposable
    {
        private MongoClient mongoClient;

        private MongoServer mongoServer;

        private MongoDatabase mongoDatabase;

        /// <summary>
        /// mongodb://[username:password@]hostname[:port][/[database][?options]]
        /// </summary>
        /// <param name="connectionString">mongodb数据库连接串</param>
        /// <param name="dataBaseName">数据库实例名称</param>
        public MongoDBOfficial(string connectionString,string dataBaseName)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (string.IsNullOrEmpty(dataBaseName))
            {
                throw new ArgumentNullException("dataBaseName");
            }
            mongoClient = new MongoClient(connectionString);
            mongoServer = mongoClient.GetServer();
            mongoDatabase = mongoServer.GetDatabase(dataBaseName);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public MongoDBOfficial(MongoClientSettings settings)
        {
            /*
            var url = new MongoUrl("mongodb://test:user@localhost:27017");
            var settings = MongoClientSettings.FromUrl(url);
            var adminCredentials = new MongoCredentials("admin", "user", true);
            */
            mongoClient=new MongoClient(settings);
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
