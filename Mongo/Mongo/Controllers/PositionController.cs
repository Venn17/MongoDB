using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mongo.Models.DataModels;
using Mongo.Models.Services;
using MongoDB.Bson;

namespace Mongo.Controllers
{
    public class PositionController : Controller
    {
        PositionService service = new PositionService();
        AreaService area = new AreaService();
        // GET: Position
        public IActionResult Index(string key, string areaID,int page = 1)
        {
            long totalPage;
            var data = service.getViewAll(key, page,areaID, 5, out totalPage);
            var areas = area.getAll();
            ViewBag.Area = areas;
            ViewBag.AreaID = areaID;
            ViewBag.Key = key;
            ViewBag.totalPage = totalPage;
            ViewBag.CurrentPage = page;
            ViewBag.Login = LoginController.getLogin();
            return View(data);
        }

        // GET: Position/Details/5
        public ActionResult Details(string id,int page = 1)
        {
            long totalPage;
            var position = service.getViewByID(id);
            var data = service.getEmpById(id,3,page,out totalPage);
            ViewBag.Position = position;
            ViewBag.totalPage = totalPage;
            ViewBag.CurrentPage = page;
            ViewBag.Login = LoginController.getLogin();
            return View(data);
        }

        // GET: Position/Create
        public ActionResult Create()
        {
            ViewBag.Login = LoginController.getLogin();
            return View();
        }

        // POST: Position/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insert(Position data)
        {
            try
            {
                BsonObjectId bsonObjectID = ObjectId.GenerateNewId();
                data._id = ObjectId.Parse(bsonObjectID.AsObjectId.ToString()).ToString();
                service.insert(data);
            }
            catch
            {
               
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Position/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.Login = LoginController.getLogin();
            var data = service.getById(id);
            return View(data);
        }

        // POST: Position/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Position data)
        {
            try
            {
                service.update(data);
            }
            catch
            {
                
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Position/Delete/5
        public ActionResult Delete(string id)
        {
            if(service.getCountEmp(id) == 0)
            {
                service.delete(id);
            }
            else
            {
                ViewBag.Message = "This position have " + service.getCountEmp(id) + " employees !!! CAN'T DELETE !";
            }
            long totalPage;
            var data = service.getViewAll("", 1, "", 5, out totalPage);
            var areas = area.getAll();
            ViewBag.Area = areas;
            ViewBag.AreaID = "";
            ViewBag.Key = "";
            ViewBag.totalPage = totalPage;
            ViewBag.CurrentPage = 1;
            ViewBag.Login = LoginController.getLogin();
            return View("Index",data);
        }
    }
}