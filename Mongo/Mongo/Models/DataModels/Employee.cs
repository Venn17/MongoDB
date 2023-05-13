using Microsoft.AspNetCore.Http;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.Models.DataModels
{
    public class Employee
    {
        [BsonId]
        public string _id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string image { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public int age { get; set; }
        public bool gender { get; set; }
        public string address { get; set; }
        public int experience { get; set; }
        public string description { get; set; }
        public string departmentID { get; set; }
        public string areaID { get; set; }
        public string positionID { get; set; }
    }
}
