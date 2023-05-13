using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.Models.DataModels
{
    public class Account
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string _id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int role { get; set; }
        public bool status { get; set; }
    }
}
