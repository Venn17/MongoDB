using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.Models.DataModels
{
    public class Employee
    {
        public int _id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public int age { get; set; }
        public bool gender { get; set; }
        public string address { get; set; }
        public int experience { get; set; }
        public int departmentID { get; set; }
        public int areaID { get; set; }
        public int positionID { get; set; }
    }
}
