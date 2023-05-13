using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mongo.Models.BusinessModels;
using Mongo.Models.DataModels;
using Mongo.Models.Services;
using MongoDB.Bson;

namespace Mongo.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentService department = new DepartmentService();

        public IActionResult Index(int page = 1)
        {
            long row;
            var data = department.getViewAll(page,3,out row);
            ViewBag.totalPage = row;
            ViewBag.CurrentPage = page;
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(Department data)
        {
            BsonObjectId bsonObjectID = ObjectId.GenerateNewId();
            data._id = ObjectId.Parse(bsonObjectID.AsObjectId.ToString()).ToString();
            department.insert(data);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string id)
        {
            var depart = department.getById(id);
            return View(depart);
        }

        [HttpPost]
        public IActionResult Update(Department dep)
        {
            department.update(dep);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            long count = department.getCountEmp(id);
            if (count == 0) {
                department.delete(id);
            }
            else
            {
                ViewBag.Message = "This department have " + count + " employees , Can not DELETE !!";
            }
            long row;
            var data = department.getViewAll(1, 3, out row);
            ViewBag.totalPage = row;
            ViewBag.CurrentPage = 1;
            return View("Index",data);

        }

        public IActionResult Search(string key, int page = 1)
        {
            if (key == null || key == "")
            {
                return RedirectToAction("Index");
            }
            else
            {
                long row;
                var data = department.getViewAllandSearch(key, page, 3, out row);
                ViewBag.totalPage = row;
                ViewBag.CurrentPage = page;
                ViewBag.Key = key;
                return View(data);
            }
        }
    }
}