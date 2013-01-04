// -----------------------------------------------------------------------
// <copyright file="MongoDBTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.Serialization;
using DotNet.Data;

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

    [DataContract]
    public class ShoppingCartEntity
    {
        [DataMember]
        public string CartId { get; set; }

        [BsonIgnore]
        public string Ha { get; set; }
    }
}
