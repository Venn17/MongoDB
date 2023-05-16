using Mongo.Models.BusinessModels;
using Mongo.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Mongo.Models.ViewModels;

namespace Mongo.Models.Services
{
    public class DepartmentService : DepartmentRepository
    {
        TestDBContext context = new TestDBContext();

        public DepartmentService(TestDBContext context)
        {
            this.context = context;
        }

        public DepartmentService()
        {
        }

        public bool delete(string key)
        {
            try
            {
                context.Departments.DeleteOne(x => x._id == key);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<Department> getAll()
        {
            var departments = context.Departments.Find(FilterDefinition<Department>.Empty).ToList();
            return departments;
        }

        public List<DepartmentViewModel> getViewAll(int page, int size, out long totalPage)
        {   
            int skip = size * (page - 1);
            long rows = context.Departments.CountDocuments(FilterDefinition<Department>.Empty);
            totalPage = rows % size == 0 ? rows / size : rows / size + 1;
            var departments = context.Departments.Find(FilterDefinition<Department>.Empty).Skip(skip).Limit(size).ToList();
            List<DepartmentViewModel> departmentViewModels = new List<DepartmentViewModel>();
            foreach (var item in departments)
            {
                var dep = new DepartmentViewModel();
                dep._id = item._id;
                dep.name = item.name;
                dep.status = item.status;
                dep.count = context.Employees.CountDocuments(x => x.departmentID == item._id);
                var leads = context.Employees.Find(x => x.departmentID == item._id && x.positionID == "po09").ToList();
                var data = "";
                if (leads.Count() > 0)
                {
                    foreach (var i in leads)
                    {
                        data += i.firstName + " " + i.lastName + ",";
                    }
                    data = data.Substring(0, data.Length - 1);
                }
                dep.leader = data;
                departmentViewModels.Add(dep);
            }
            return departmentViewModels;
        }

        public List<DepartmentViewModel> getViewAllandSearch(string name,int page, int size, out long totalPage)
        {
            if(name == null)
            {
                name = "";
            }
            int skip = size * (page - 1);
            long rows = context.Departments.CountDocuments(x => x.name.ToLower().Contains(name.ToLower()));
            totalPage = rows % size == 0 ? rows / size : rows / size + 1;
            var departments = context.Departments.Find(x => x.name.ToLower().Contains(name.ToLower())).Skip(skip).Limit(size).ToList();
            List<DepartmentViewModel> departmentViewModels = new List<DepartmentViewModel>();
            foreach (var item in departments)
            {
                var dep = new DepartmentViewModel();
                dep._id = item._id;
                dep.name = item.name;
                dep.status = item.status;
                dep.count = context.Employees.CountDocuments(x => x.departmentID == item._id);
                var leads = context.Employees.Find(x => x.departmentID == item._id && x.positionID == "4").ToList();
                var data = "";
                if (leads.Count() > 0)
                {
                    foreach (var i in leads)
                    {
                        data += i.firstName + " " + i.lastName + ",";
                    }
                    data = data.Substring(0, data.Length - 1);
                }
                dep.leader = data;
                departmentViewModels.Add(dep);
            }
            return departmentViewModels;
        }

        public Department getById(string key)
        {
            var department = context.Departments.Find(x => x._id == key).FirstOrDefault();
            return department;
        }

        public bool insert(Department entity)
        {
            try
            {
                entity.status = entity.status ? entity.status : false;
                context.Departments.InsertOne(entity);
            }catch(Exception)
            {
                return false;
            }
            return true;
        }

        public List<Department> pagination(int page, int size, out long totalPage)
        {
            totalPage = 0;
            return null;
        }

        public List<Department> searchPagination(string name, int page, int size, out long totalPage)
        {
            totalPage = 0;
            return null;
        }

        public bool update(Department entity)
        {
            try
            {
                var data = Builders<Department>.Update
                    .Set("name", entity.name)
                    .Set("status", entity.status);
                context.Departments.UpdateOne(x => x._id == entity._id,data);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public long getCountEmp(string id)
        {
            var count = context.Employees.CountDocuments(x => x.departmentID == id);
            return count;
        }
    }
}
