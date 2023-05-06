using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mongo.Models.BusinessModels;
using Mongo.Models.Services;

namespace Mongo.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentRepository department;
        EmployeeRepository employee;
        AreaRepository area;
        PositionRepository position;

        public DepartmentController(DepartmentRepository department, EmployeeRepository employee, AreaRepository area, PositionRepository position)
        {
            this.department = department;
            this.employee = employee;
            this.area = area;
            this.position = position;
        }

        public IActionResult Index()
        {
            var data = department.getAll();
            return View(data);
        }
    }
}