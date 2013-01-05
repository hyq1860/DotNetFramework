// -----------------------------------------------------------------------
// <copyright file="MongoDBTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.Serialization;
using DotNet.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace DotNetTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// mongodb测试
    /// </summary>
    public class MongoDBTest
    {
        public static void Insert(ShoppingCartEntity cart)
        {
            using (MongoDBHelper mongoDb = new MongoDBHelper("Server=127.0.0.1:27017", "ShoppingCart"))
            {
                mongoDb.GetCollection<ShoppingCartEntity>().Insert(cart);
            }
        }

        public static ShoppingCartEntity GetById(string cartId)
        {
            using (MongoDBHelper mongoDb = new MongoDBHelper("Server=127.0.0.1:27017", "ShoppingCart"))
            {
                var data=mongoDb.GetCollection<ShoppingCartEntity>().FindOne(new { CartId = cartId });
                return data;
            }
        }

        public static void Update(ShoppingCartEntity cart)
        {
            using (MongoDBHelper mm = new MongoDBHelper())
            {
                mm.GetCollection<ShoppingCartEntity>().Update(cart, new { CartId = cart.CartId });
            }
        }

       
    }

    public class MongoDBOfficialTest
    {
        

        public static ShoppingCartEntity GetById(ObjectId objectId)
        {
            using (MongoDBOfficial mongoDb = new MongoDBOfficial("Server=127.0.0.1:27017", "ShoppingCart"))
            {
                IMongoQuery query = Query.EQ("_id", objectId);
                var data = mongoDb.GetCollection<ShoppingCartEntity>("ShoppingCart").FindOne(query);
                return data;
            }
        }

        public static ObjectId Insert(ShoppingCartEntity cart)
        {
            using (MongoDBOfficial mongoDb = new MongoDBOfficial("Server=127.0.0.1:27017", "ShoppingCart"))
            {

                var data =
                    mongoDb.GetCollection<ShoppingCartEntity>("ShoppingCart")
                           .Insert(cart);
                return cart.Id;
            }
        }
    }

    [DataContract]
    [BsonIgnoreExtraElements]
    public class ShoppingCartEntity
    {
        [DataMember]
        public string CartId { get; set; }

        public ObjectId Id { get; set; }

        public string Ha { get; set; }

        public PromotionEntity Promotion { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class PromotionEntity
    {
        public string Date1 { get; set; }

        //public List<string> Date2 { get; set; } 
    }
}
