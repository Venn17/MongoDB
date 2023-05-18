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
    public class AreaService : AreaRepository
    {
        TestDBContext context = new TestDBContext();

        public AreaService()
        {
        }

        public AreaService(TestDBContext context)
        {
            this.context = context;
        }

        public bool delete(string key)
        {
            try
            {
                context.Areas.DeleteOne(x => x._id == key);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<Area> getAll()
        {
            var Areas = context.Areas.Find(FilterDefinition<Area>.Empty).ToList();
            return Areas;
        }

        public Area getById(string key)
        {
            var Area = context.Areas.Find(x => x._id == key).FirstOrDefault();
            return Area;
        }

        public bool insert(Area entity)
        {
            try
            {
                entity.status = entity.status ? entity.status : false;
                context.Areas.InsertOne(entity);
            }catch(Exception)
            {
                return false;
            }
            return true;
        }

        internal object getViewAllandSearch(string name, int page, int size, out long totalPage)
        {
            if (name == null)
            {
                name = "";
            }
            int skip = size * (page - 1);
            long rows = context.Areas.CountDocuments(x => x.name.ToLower().Contains(name.ToLower()));
            totalPage = rows % size == 0 ? rows / size : rows / size + 1;
            var areas = context.Areas.Find(x => x.name.ToLower().Contains(name.ToLower())).Skip(skip).Limit(size).ToList();
            List<AreaViewModel> areaViews = new List<AreaViewModel>();
            foreach (var item in areas)
            {
                var area = new AreaViewModel();
                area._id = item._id;
                area.name = item.name;
                area.status = item.status;
                area.count = context.Employees.CountDocuments(x => x.areaID == item._id);
                areaViews.Add(area);
            }
            return areaViews;
        }

        public List<Area> pagination(int page, int size, out long totalPage)
        {
            int skip = size * (page - 1);
            long rows = context.Areas.CountDocuments(FilterDefinition<Area>.Empty);
            totalPage = rows % size == 0 ? rows / size : rows / size + 1;
            return context.Areas.Find(FilterDefinition<Area>.Empty).Skip(skip).Limit(size).ToList();
        }

        public List<Area> searchPagination(string name, int page, int size, out long totalPage)
        {
            int skip = size * (page - 1);
            long rows = context.Areas.CountDocuments(x => x.name.ToLower().Contains(name.ToLower()));
            totalPage = rows % size == 0 ? rows / size : rows / size + 1;
            return context.Areas.Find(x => x.name.ToLower().Contains(name.ToLower())).Skip(skip).Limit(size).ToList();
        }

        public bool update(Area entity)
        {
            try
            {
                var data = Builders<Area>.Update
                    .Set("name", entity.name)
                    .Set("status", entity.status);
                context.Areas.UpdateOne(x => x._id == entity._id,data);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<AreaViewModel> getViewAll(int page,int size,out long totalPage)
        {
            int skip = size * (page - 1);
            long rows = context.Areas.CountDocuments(FilterDefinition<Area>.Empty);
            totalPage = rows % size == 0 ? rows / size : rows / size + 1;
            var areas = context.Areas.Find(FilterDefinition<Area>.Empty).Skip(skip).Limit(size).ToList();
            List<AreaViewModel> areaViews = new List<AreaViewModel>();
            foreach (var item in areas)
            {
                var area = new AreaViewModel();
                area._id = item._id;
                area.name = item.name;
                area.status = item.status;
                area.count = context.Employees.CountDocuments(x => x.areaID == item._id);
                areaViews.Add(area);
            }
            return areaViews;
        }

        public long getCountEmp(string id)
        {
            var count = context.Employees.CountDocuments(x => x.areaID == id);
            return count;
        }
    }
}
