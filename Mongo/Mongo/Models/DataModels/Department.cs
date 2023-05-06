using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.Models.DataModels
{
    public class Department
    {
        public int _id { get; set; }
        public string name { get; set; }
        public bool status { get; set; }
    }
}
