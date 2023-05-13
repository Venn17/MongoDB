using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public string _id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public int age { get; set; }
        public string image { get; set; }
        public bool gender { get; set; }
        public string address { get; set; }
        public int experience { get; set; }
        public string description { get; set; }
        public string departmentName { get; set; }
        public string areaName { get; set; }
        public string positionName { get; set; }
    }
}
