using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mongo.Models.DataModels;
using Mongo.Models.Services;
using MongoDB.Bson;

namespace Mongo.Controllers
{
    public class LoginController : Controller
    {
        private static Account logined;
        AccountService service = new AccountService();
        static AccountService s = new AccountService();
        public IActionResult Index()
        {
            return View("Views/Home/Login.cshtml");
        }

        public static Account getLogin()
        {
            var data = s.getProfile();
            if(s.getProfile() == null)
            {
                return null;
            }
            logined = data;
            return logined;
        }
        [HttpPost]
        public IActionResult Login(string name,string pass)
        {
           var data = service.checkLogin(name, pass);
           if (data == null)
            {
                ViewBag.Message = "Email or Password NOT correct !! Try Again ";
                return View("/Views/Home/Login.cshtml");
            }
            ViewBag.Login = data;
            return View("/Views/Home/Index.cshtml");
        }

        public IActionResult Logout()
        {
            service.Logout();
            return RedirectToAction("Index");
        }

        public IActionResult Profile()
        {
            ViewBag.Login = LoginController.getLogin();
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
            if(email == "")
            {
                ViewBag.Message = "Email field is Not Empty !! Try Again";
            }
            else
            {
                if (Regex.IsMatch(email, regex))
                {
                    service.forgotPassword(email);
                    return View();
                }
                else
                {
                    ViewBag.Message = "Email invalidate !! Try Again";
                }
            }
            return View("forget");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(string name,string email,string password,string confirm)
        {
            if(name == null)
            {
                ViewBag.NameRequire = "Name is REQUIRED !!";
                return View("Register");
            }
            if (email == null)
            {
                ViewBag.EmailRequire = "Email is REQUIRED !!";
                return View("Register");
            }
            if (password == null)
            {
                ViewBag.PassRequire = "Password is REQUIRED !!";
                return View("Register");
            }
            if (confirm == null)
            {
                ViewBag.ConfirmRequire = "Name is REQUIRED !!";
                return View("Register");
            }
            if(password != confirm)
            {
                ViewBag.ConfirmPass = "Confirm Password is INCORRECT !!";
                return View("Register");
            }
            Account a = new Account();
            BsonObjectId bsonObjectID = ObjectId.GenerateNewId();
            a._id = ObjectId.Parse(bsonObjectID.AsObjectId.ToString()).ToString();
            a.name = name;
            a.email = email;
            a.password = password;
            a.status = true;
            a.role = 1;
            service.register(a);
            ViewBag.Ok = "Register Successfully !!!";
            return View("Views/Home/Login.cshtml");
        }

        public IActionResult ChangeProfile()
        {
            var data = service.getProfile();
            ViewBag.Login = LoginController.getLogin();
            return View();
        }

        [HttpPost]
        public IActionResult UpdateProfile(Account a)
        {
            service.update(a);
            return RedirectToAction("Profile");
        }

        public IActionResult ChangePass()
        {
            ViewBag.Login = LoginController.getLogin();
            return View();
        }

        [HttpPost]
        public IActionResult UpdatePass(string pass)
        {
            var data = getLogin();
            service.updatePass(data, pass);
            return View("/Views/Home/Index.cshtml");
        }
    }
}