using Mongo.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using Mongo.Models.DataModels;
using Mongo.Models.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Mongo.Models.Services
{
    public class EmployeeService : EmployeeRepository
    {
        TestDBContext context = new TestDBContext();

        public EmployeeService(TestDBContext context)
        {
            this.context = context;
        }

        public EmployeeService()
        {
        }

        public bool delete(string key)
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

        public Employee getById(string key)
        {
            var data = context.Employees.Find(x => x._id == key).FirstOrDefault();
            return data;
        }

        public List<EmployeeViewModel> getInfomation(int page , int size,out long row)
        {
            //BsonDocument[] joinTable = new BsonDocument[3]
            //{
            //    new BsonDocument
            //    {
            //        {
            //            "$lookup",new BsonDocument
            //            {
            //                {"from","departments" },
            //                {"localField","departmentID"},
            //                {"foreignField","_id"},
            //                {"as","departmentName"}
            //            }
            //        }
            //    },
            //    new BsonDocument
            //    {
            //        {
            //            "$lookup",new BsonDocument
            //            {
            //                {"from","areas" },
            //                {"localField","areaID"},
            //                {"foreignField","_id"},
            //                {"as","areaName"}
            //            }
            //        }
            //    },
            //    new BsonDocument
            //    {
            //        {
            //            "$lookup",new BsonDocument
            //            {
            //                {"from","positions" },
            //                {"localField","positionID"},
            //                {"foreignField","_id"},
            //                {"as","positionName"}
            //            }
            //        }
            //    }
            //};
            int skip = size * (page - 1);
            long rows = context.Employees.CountDocuments(FilterDefinition<Employee>.Empty);
            row = rows % size == 0 ? rows / size : rows / size + 1;
            var emps = context.Employees.Find(FilterDefinition<Employee>.Empty).Skip(skip).Limit(size).ToList();
            var data = new List<EmployeeViewModel>();
            foreach (var item in emps)
            {
                var e = new EmployeeViewModel();
                e._id = item._id;
                e.firstName = item.firstName;
                e.lastName = item.lastName;
                e.email = item.email;
                e.phone = item.phone;
                e.age = item.age;
                e.address = item.address;
                e.experience = item.experience;
                var dep = context.Departments.Find(x => x._id == item.departmentID).FirstOrDefault();
                e.departmentName = dep.name;
                var po = context.Positions.Find(x => x._id == item.positionID).FirstOrDefault();
                e.positionName = po.name;
                var area = context.Areas.Find(x => x._id == item.areaID).FirstOrDefault();
                e.areaName = area.name;
                data.Add(e);
            }
            return data;
        }


        public async Task<bool> insertAsync([FromForm] EmployeeModel model)
        {
            Employee e = new Employee();
            e._id = model._id;
            e.firstName = model.firstName;
            e.lastName = model.lastName;
            e.age = model.age;
            e.email = model.email;
            e.phone = model.phone;
            e.gender = model.gender;
            e.description = model.description;
            e.departmentID = model.departmentID;
            e.areaID = model.areaID;
            e.positionID = model.positionID;
            e.experience = model.experience;
            e.address = model.address;
            //upload anh
            if (model.image.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", model.image.FileName);
                using (var stream = File.Create(path))
                {
                    await model.image.CopyToAsync(stream);
                }
                e.image = model.image.FileName;
            }
            else
            {
                e.image = "";
            }
            context.Employees.InsertOne(e);
            return false;
        }

        public bool insert(Employee entity)
        {
            throw new NotImplementedException();
        }

        public List<Employee> pagination(int page, int size, out long totalPage)
        {
            int skip = size * (page - 1);
            long rows = context.Employees.CountDocuments(FilterDefinition<Employee>.Empty);
            totalPage = rows % size == 0 ? rows / size : rows / size + 1;
            return context.Employees.Find(FilterDefinition<Employee>.Empty).Skip(skip).Limit(size).ToList();
        }

        public List<Employee> searchPagination(string name, int page, int size, out long totalPage)
        {
            int skip = size * (page - 1);
            long rows = context.Employees.CountDocuments(x => (x.firstName + x.lastName).ToLower().Contains(name.ToLower()));
            totalPage = rows % size == 0 ? rows / size : rows / size + 1;
            return context.Employees.Find(x => (x.firstName + x.lastName).ToLower().Contains(name.ToLower())).Skip(skip).Limit(size).ToList();
        }

        public bool update(Employee entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateOptions([FromForm] EmployeeModel model)
        {
            try
            {
                var data = Builders<Employee>.Update
                    .Set("firstName", model.firstName)
                    .Set("lastName", model.lastName)
                    .Set("email", model.email)
                    .Set("phone", model.phone)
                    .Set("gender", model.gender)
                    .Set("age", model.age)
                    .Set("address", model.address)
                    .Set("experience", model.experience)
                    .Set("departmentID", model.departmentID)
                    .Set("areaID", model.areaID)
                    .Set("positionID", model.positionID);
                context.Employees.UpdateOne(x => x._id == model._id, data);
                if (model.image.Length > 0)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", model.image.FileName);
                    using (var stream = File.Create(path))
                    {
                        await model.image.CopyToAsync(stream);
                    }
                    data.Set("image",model.image.FileName);
                }
                else
                {
                    data.Set("image","");
                }
                context.Employees.UpdateOne(x => x._id == model._id,data);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public EmployeeViewModel getAllById(string key)
        {
            EmployeeViewModel e = new EmployeeViewModel();
            var model = context.Employees.Find(x => x._id == key).FirstOrDefault();
            e._id = model._id;
            e.firstName = model.firstName;
            e.lastName = model.lastName;
            e.age = model.age;
            e.image = model.image;
            e.email = model.email;
            e.phone = model.phone;
            e.gender = model.gender;
            e.description = model.description;
            e.experience = model.experience;
            e.address = model.address;
            var dep = context.Departments.Find(x => x._id == model.departmentID).FirstOrDefault();
            e.departmentName = dep.name;
            var po = context.Positions.Find(x => x._id == model.positionID).FirstOrDefault();
            e.positionName = po.name;
            var area = context.Areas.Find(x => x._id == model.areaID).FirstOrDefault();
            e.areaName = area.name;
            return e;
        }
    }
}
