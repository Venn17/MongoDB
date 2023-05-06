using Mongo.Models.BusinessModels;
using Mongo.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.Models.Services
{
    public class PositionService : PositionRepository
    {
        public bool delete(int key)
        {
            throw new NotImplementedException();
        }

        public List<Position> getAll()
        {
            throw new NotImplementedException();
        }

        public Position getById(int key)
        {
            throw new NotImplementedException();
        }

        public bool insert(Position entity)
        {
            throw new NotImplementedException();
        }

        public List<Position> pagination(int page, int size, out long totalPage)
        {
            throw new NotImplementedException();
        }

        public List<Position> searchPagination(string name, int page, int size, out long totalPage)
        {
            throw new NotImplementedException();
        }

        public bool update(Position entity)
        {
            throw new NotImplementedException();
        }
    }
}
