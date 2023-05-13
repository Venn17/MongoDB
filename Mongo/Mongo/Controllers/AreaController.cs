using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mongo.Models.DataModels;
using Mongo.Models.Services;
using MongoDB.Bson;

namespace Mongo.Controllers
{
    public class AreaController : Controller
    {
        AreaService area = new AreaService();
        public IActionResult Index(int page = 1)
        {
            long row;
            var data = area.getViewAll(page, 3, out row);
            ViewBag.totalPage = row;
            ViewBag.CurrentPage = page;
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(Area data)
        {
            BsonObjectId bsonObjectID = ObjectId.GenerateNewId();
            data._id = ObjectId.Parse(bsonObjectID.AsObjectId.ToString()).ToString();
            area.insert(data);
            return RedirectToAction("Index");
        }

        public IActionResult Search(string key,int page = 1)
        {
            if (key == null || key == "")
            {
                return RedirectToAction("Index");
            }
            else
            {
                long row;
                var data = area.getViewAllandSearch(key, page, 3, out row);
                ViewBag.totalPage = row;
                ViewBag.CurrentPage = page;
                ViewBag.Key = key;
                return View(data);
            }
        }

        public IActionResult Edit(string id)
        {
            var a = area.getById(id);
            return View(a);
        }

        [HttpPost]
        public IActionResult Update(Area a)
        {
            area.update(a);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            long count = area.getCountEmp(id);
            if (count == 0)
            {
                area.delete(id);
            }
            else
            {
                ViewBag.Message = "This area have " + count + " employees , Can not DELETE !!";
            }
            long row;
            var data = area.getViewAll(1, 3, out row);
            ViewBag.totalPage = row;
            ViewBag.CurrentPage = 1;
            return View("Index", data);

        }
    }
}