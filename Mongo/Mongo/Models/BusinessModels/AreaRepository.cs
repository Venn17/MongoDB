using Mongo.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.Models.BusinessModels
{
    public interface AreaRepository : IGenericRepository<Area,int>
    {
        List<Area> pagination(int page, int size, out long totalPage);
        List<Area> searchPagination(string name, int page, int size, out long totalPage);
    }
}
