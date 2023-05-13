﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.Models.DataModels
{
    public class Position
    {
        [BsonId]
        public string _id { get; set; }
        public string name { get; set; }
    }
}
