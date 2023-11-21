using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserApp.Models;
using UserApp.Models.VM;

namespace UserApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<VMRegister> model = new List<VMRegister>();

            using (UserAppEntities db = new UserAppEntities())
            {
                var users = db.UserRegisters;
                foreach (var user in users)
                {
                    VMRegister vmUser = new VMRegister()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        CreatedOn = user.CreateOn.ToString("dd mm yyyy"),
                        LastLoggedOn = user.LastLoggedOn?.ToString("dd mm yyyy") ?? "NA"
                    };
                    model.Add(vmUser);
                }
                
            }
            return View(model);
        }
    }
}