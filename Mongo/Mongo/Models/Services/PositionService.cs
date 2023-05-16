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
    public class PositionService : PositionRepository
    {
        TestDBContext context = new TestDBContext();

        public PositionService()
        {
        }

        public PositionService(TestDBContext context)
        {
            this.context = context;
        }

        public bool delete(string key)
        {
            try
            {
                context.Positions.DeleteOne(x => x._id == key);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<Position> getAll()
        {
            var Positions = context.Positions.Find(FilterDefinition<Position>.Empty).ToList();
            return Positions;
        }

        public List<PositionViewModel> getViewAll(string name,int page,string area,int size, out long totalpage)
        {
            int skip = size * (page - 1);
            var pos = context.Positions.Find(FilterDefinition<Position>.Empty).Skip(skip).Limit(size).ToList();
            long rows = context.Positions.CountDocuments(FilterDefinition<Position>.Empty);
            totalpage = rows % size == 0 ? rows / size : rows / size + 1;
            List<PositionViewModel> list = new List<PositionViewModel>();
            if (name == null || name == "")
            {
                if(area == null || area == "")
                {
                    foreach (var item in pos)
                    {
                        PositionViewModel p = new PositionViewModel();
                        p._id = item._id;
                        p.name = item.name;
                        p.count = context.Employees.CountDocuments(x => x.positionID == item._id);
                        list.Add(p);
                    }
                }
                else
                {
                    foreach (var item in pos)
                    {
                        PositionViewModel p = new PositionViewModel();
                        p._id = item._id;
                        p.name = item.name;
                        p.count = context.Employees.CountDocuments(x => x.areaID == area & x.positionID == item._id);
                        list.Add(p);
                    }
                }
            }
            else
            {
                if(area == "")
                {
                    pos = context.Positions.Find(x => x.name.ToLower().Contains(name.ToLower())).Skip(skip).Limit(size).ToList();
                    rows = context.Positions.CountDocuments(x => x.name.ToLower().Contains(name.ToLower()));
                    totalpage = rows % size == 0 ? rows / size : rows / size + 1;
                    foreach (var item in pos)
                    {
                        PositionViewModel p = new PositionViewModel();
                        p._id = item._id;
                        p.name = item.name;
                        p.count = context.Employees.CountDocuments(x => x.positionID == item._id);
                        list.Add(p);
                    }
                }
                else
                {
                    pos = context.Positions.Find(x => x.name.ToLower().Contains(name.ToLower())).Skip(skip).Limit(size).ToList();
                    rows = context.Positions.CountDocuments(x => x.name.ToLower().Contains(name.ToLower()));
                    totalpage = rows % size == 0 ? rows / size : rows / size + 1;
                    foreach (var item in pos)
                    {
                        PositionViewModel p = new PositionViewModel();
                        p._id = item._id;
                        p.name = item.name;
                        p.count = context.Employees.CountDocuments(x => x.positionID == item._id & x.areaID == area);
                        list.Add(p);
                    }
                }
            }
            return list;
        }

        public Position getById(string key)
        {
            var Position = context.Positions.Find(x => x._id == key).FirstOrDefault();
            return Position;
        }

        public PositionViewModel getViewByID(string id)
        {
            var pos = context.Positions.Find(x => x._id == id).FirstOrDefault();
            PositionViewModel p = new PositionViewModel();
            p._id = pos._id;
            p.name = pos.name;
            p.count = context.Employees.CountDocuments(x => x.positionID == id);
            return p;
        }

        public long getCountEmp(string id)
        {
            return context.Employees.CountDocuments(x => x.positionID == id);
        }

        public List<EmployeeViewModel> getEmpById(string id, int size, int page,out long row)
        {
            int skip = size * (page - 1);
            long rows = context.Employees.CountDocuments(x => x.positionID == id);
            row = rows % size == 0 ? rows / size : rows / size + 1;
            var emps = context.Employees.Find(x => x.positionID == id).Skip(skip).Limit(size).ToList();
            var data = new List<EmployeeViewModel>();
            foreach (var item in emps)
            {
                var e = new EmployeeViewModel();
                e._id = item._id;
                e.firstName = item.firstName;
                e.lastName = item.lastName;
                e.email = item.email;
                e.phone = item.phone;
                e.image = item.image;
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

        public bool insert(Position entity)
        {
            try
            {
                context.Positions.InsertOne(entity);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<Position> pagination(int page, int size, out long totalPage)
        {
            int skip = size * (page - 1);
            long rows = context.Positions.CountDocuments(FilterDefinition<Position>.Empty);
            totalPage = rows % size == 0 ? rows / size : rows / size + 1;
            return context.Positions.Find(FilterDefinition<Position>.Empty).Skip(skip).Limit(size).ToList();
        }

        public List<Position> searchPagination(string name, int page, int size, out long totalPage)
        {
            int skip = size * (page - 1);
            long rows = context.Positions.CountDocuments(x => x.name.ToLower().Contains(name.ToLower()));
            totalPage = rows % size == 0 ? rows / size : rows / size + 1;
            return context.Positions.Find(x => x.name.ToLower().Contains(name.ToLower())).Skip(skip).Limit(size).ToList();
        }

        public bool update(Position entity)
        {
            try
            {
                var data = Builders<Position>.Update
                    .Set("name", entity.name);
                context.Positions.UpdateOne(x => x._id == entity._id, data);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
