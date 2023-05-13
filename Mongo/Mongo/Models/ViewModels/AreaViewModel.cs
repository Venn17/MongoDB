using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.Models.ViewModels
{
    public class AreaViewModel
    {
        public string _id { get; set; }
        public string name { get; set; }
        public long count { get; set; }
        public bool status { get; set; }
    }
}
