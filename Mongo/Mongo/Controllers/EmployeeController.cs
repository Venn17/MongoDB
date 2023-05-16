using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mongo.Models.DataModels;
using Mongo.Models.Services;

namespace Mongo.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeService employeeService = new EmployeeService();
        DepartmentService department = new DepartmentService();
        PositionService position = new PositionService();
        AreaService area = new AreaService(); 
        // GET: Employee
        public IActionResult Index(int page = 1)
        {
            long row;
            var data = employeeService.getInfomation(page, 6, out row);
            var dep = department.getAll();
            var po = position.getAll();
            var a = area.getAll();
            ViewBag.Department = dep;
            ViewBag.Position = po;
            ViewBag.Area = a;
            ViewBag.totalPage = row;
            ViewBag.CurrentPage = page;
            return View(data);
        }

        // GET: Employee/Details/5
        public ActionResult Details(string id)
        {
            var data = employeeService.getAllById(id);
            return View(data);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            var dep = department.getAll();
            var po = position.getAll();
            var a = area.getAll();
            ViewBag.Department = dep;
            ViewBag.Position = po;
            ViewBag.Area = a;
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([FromForm] EmployeeModel collection)
        {
            try
            {
                await employeeService.insertAsync(collection);
            }
            catch
            {
                
            }
            return RedirectToAction("Index");
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(string id)
        {
            var emp = employeeService.getById(id);
            var dep = department.getAll();
            var po = position.getAll();
            var a = area.getAll();
            ViewBag.Department = dep;
            ViewBag.Position = po;
            ViewBag.Area = a;
            return View(emp);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateAsync([FromForm] EmployeeModel collection)
        {
            try
            {
                await employeeService.UpdateOptions(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(string id)
        {
            employeeService.delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Search(string key,string depID, string positionID,string areaID,int page = 1)
        {
            long row;
            var data = employeeService.getInfomationAndSearch(key,depID,positionID,areaID,page, 6, out row);
            var dep = department.getAll();
            var po = position.getAll();
            var a = area.getAll();
            ViewBag.Department = dep;
            ViewBag.Position = po;
            ViewBag.Area = a;
            ViewBag.Key = key;
            ViewBag.DepartmentID = depID;
            ViewBag.PositionID = positionID;
            ViewBag.AreaID = areaID;
            ViewBag.totalPage = row;
            ViewBag.CurrentPage = page;
            return View(data);
        }
    }
}