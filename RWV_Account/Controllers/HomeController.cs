using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RWV_Account.Models;

namespace RWV_Account.Controllers
{
    public class HomeController : Controller
    {
        RW_Vaals_AccountEntities db = new RW_Vaals_AccountEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserList()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(T_Users t_Users)
        {
            var checkLogin = db.T_Users.Where(x => x.Email.Equals(t_Users.Email) && x.Password.Equals(t_Users.Password)).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["id"] = t_Users.id.ToString();
                Session["Email"] = t_Users.Email.ToString();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notification = "Wrong Email or Password.";
            }
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(T_Users t_Users)
        {
            if (db.T_Users.Any(x=>x.Email == t_Users.Email))
            {
                ViewBag.Notification = "This account already exists";
                return View();
            }
            else
            {
                db.T_Users.Add(t_Users);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            
        }
    }
}