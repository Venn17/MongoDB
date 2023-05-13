using Mongo.Models.BusinessModels;
using Mongo.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

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

        public Position getById(string key)
        {
            var Position = context.Positions.Find(x => x._id == key).FirstOrDefault();
            return Position;
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
