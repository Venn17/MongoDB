using Mongo.Models.BusinessModels;
using Mongo.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Mongo.Models.Services
{
    public class DepartmentService : DepartmentRepository
    {
        TestDBContext context;

        public DepartmentService(TestDBContext context)
        {
            this.context = context;
        }

        public bool delete(int key)
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

        public Department getById(int key)
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
            int skip = size * (page - 1);
            long rows = context.Departments.CountDocuments(FilterDefinition<Department>.Empty);
            totalPage = rows % size == 0 ? rows / size : rows / size + 1;
            return context.Departments.Find(FilterDefinition<Department>.Empty).Skip(skip).Limit(size).ToList();
        }

        public List<Department> searchPagination(string name, int page, int size, out long totalPage)
        {
            int skip = size * (page - 1);
            long rows = context.Departments.CountDocuments(x => x.name.ToLower().Contains(name.ToLower()));
            totalPage = rows % size == 0 ? rows / size : rows / size + 1;
            return context.Departments.Find(x => x.name.ToLower().Contains(name.ToLower())).Skip(skip).Limit(size).ToList();
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
    }
}
