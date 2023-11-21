using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using UserApp.Models;
using UserApp.Models.VM;

namespace UserApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Register()
        { return View(); }
        [HttpPost]
        public ActionResult Register(VMRegister register)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RegisterError = "Input data is not valid";
                return View();
            }
            using (UserAppEntities db = new UserAppEntities())
            {
                var IsExist = db.UserRegisters.Any(x => x.Email.ToLower() == register.Email.ToLower());
                if (IsExist)
                {
                    ViewBag.RegisterError = $"User already exist with emailId : {register.Email}";
                    RedirectToAction("Login");
                    return View();
                }
                UserRegister userRegister = new UserRegister()
                {
                    Id = Guid.NewGuid(),
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    Password = register.Password,
                    CreateOn = DateTime.Now
                };
                db.UserRegisters.Add(userRegister);
                db.SaveChanges();
                ViewBag.RegisterSuccess = "User added Successfully";
                ModelState.Clear();
            }
            return View();
        }
        public ActionResult Login() 
        { 
            return View(); 
        }
    }
}