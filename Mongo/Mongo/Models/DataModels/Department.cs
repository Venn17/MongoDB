using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Mongo.Models.DataModels
{
    public class Department
    {
        [BsonId]
        public string _id { get; set; }
        public string name { get; set; }
        public bool status { get; set; }
    }
}
