using Mongo.Models.BusinessModels;
using Mongo.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.Models.Services
{
    public class AreaService : AreaRepository
    {
        public bool delete(int key)
        {
            throw new NotImplementedException();
        }

        public List<Area> getAll()
        {
            throw new NotImplementedException();
        }

        public Area getById(int key)
        {
            throw new NotImplementedException();
        }

        public bool insert(Area entity)
        {
            throw new NotImplementedException();
        }

        public List<Area> pagination(int page, int size, out long totalPage)
        {
            throw new NotImplementedException();
        }

        public List<Area> searchPagination(string name, int page, int size, out long totalPage)
        {
            throw new NotImplementedException();
        }

        public bool update(Area entity)
        {
            throw new NotImplementedException();
        }
    }
}
