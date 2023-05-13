using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.Models.DataModels
{
    public class EmployeeModel
    {
        public string _id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public IFormFile image { get; set; }
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
