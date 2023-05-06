using Mongo.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using Mongo.Models.DataModels;
using Mongo.Models.ViewModels;

namespace Mongo.Models.Services
{
    public class EmployeeService : EmployeeRepository
    {
        TestDBContext context;

        public EmployeeService(TestDBContext context)
        {
            this.context = context;
        }

        public bool delete(int key)
        {
            try
            {
                context.Employees.DeleteOne(x => x._id == key);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<Employee> getAll()
        {
            var data = context.Employees.Find(FilterDefinition<Employee>.Empty).ToList();
            return data;
        }

        public Employee getById(int key)
        {
            var data = context.Employees.Find(x => x._id == key).FirstOrDefault();
            return data;
        }

        public List<EmployeeViewModel> getInfomation()
        {
            BsonDocument[] joinTable = new BsonDocument[3]
            {
                new BsonDocument
                {
                    {
                        "$lookup",new BsonDocument
                        {
                            {"from","departments" },
                            {"localField","departmentID"},
                            {"foreignField","_id"}, 
                            {"as","departmentName"}
                        }
                    }
                },
                new BsonDocument
                {
                    {
                        "$lookup",new BsonDocument
                        {
                            {"from","areas" },
                            {"localField","areaID"},
                            {"foreignField","_id"}, 
                            {"as","areaName"}
                        }
                    }
                },
                new BsonDocument
                {
                    {
                        "$lookup",new BsonDocument
                        {
                            {"from","positions" },
                            {"localField","positionID"},
                            {"foreignField","_id"}, 
                            {"as","positionName"}
                        }
                    }
                }
            };
            var emps = context.Employees.Aggregate<BsonDocument>(joinTable).ToList();
            var data = new List<EmployeeViewModel>();
            foreach (var item in emps)
            {
                var e = new EmployeeViewModel();
                e._id = item["_id"].ToInt32();
                e.firstName = item["firstName"].ToString();
                e.lastName = item["lastName"].ToString();
                e.email = item["email"].ToString();
                e.phone = item["phone"].ToString();
                e.age = item["age"].ToInt32();
                e.address = item["address"].ToString();
                e.experience = item["experience"].ToInt32();
                e.departmentName = item["departments"].AsBsonArray[0]["departmentName"].ToString();
                e.positionName = item["positions"].AsBsonArray[0]["positionName"].ToString();
                e.areaName = item["areas"].AsBsonArray[0]["areaName"].ToString();
                data.Add(e);
            }
            return data;
        }
            

        public bool insert(Employee entity)
        {
            throw new NotImplementedException();
        }

        public List<Employee> pagination(int page, int size, out long totalPage)
        {
            throw new NotImplementedException();
        }

        public List<Employee> searchPagination(string name, int page, int size, out long totalPage)
        {
            throw new NotImplementedException();
        }

        public bool update(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
