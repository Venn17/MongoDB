using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mongo.Models.Services;

namespace Mongo.Controllers
{
    public class LoginController : Controller
    {
        AccountService service = new AccountService();
        public IActionResult Index()
        {
            return View("~/Views/Login/Index.cshtml");
        }

        [HttpPost]
        public IActionResult Login(string name,string pass)
        {
           if(service.checkLogin(name, pass) == null)
            {
                ViewBag.Message = "Email or Password NOT correct !! Try Again ";
            }
            return View("Home/Index");
        }

        public IActionResult Logout()
        {
            service.Logout();
            return View("Login");
        }

        public IActionResult getInfor()
        {
            var data = service.getProfile();
            return View(data);
        }

        public IActionResult forget()
        {
            return View();
        }

        [HttpPost]
        public IActionResult forgetPass(string email)
        {
            var regex = "^\\S+@\\S+\\.\\S+$";
            if (Regex.IsMatch(email, regex))
            {
                service.forgotPassword(email);
                return View();
            }
            else
            {
                ViewBag.Message = "Email invalidate !! Try Again";
            }
            return View("forget");
        }
    }
}