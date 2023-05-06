using Mongo.Models.DataModels;
using Mongo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.Models.BusinessModels
{
    public interface EmployeeRepository : IGenericRepository<Employee,int> 
    {
        List<Employee> pagination(int page, int size, out long totalPage);
        List<Employee> searchPagination(string name,int page, int size, out long totalPage);
        List<EmployeeViewModel> getInfomation();
    }
}
