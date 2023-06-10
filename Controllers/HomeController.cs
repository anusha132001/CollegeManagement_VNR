using CollegeManagement_VNR.Models;
using CollegeManagement_VNR;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CollegeManagement_VNR.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult home()
        {
            return View();
        }
        public ActionResult logindemo()
        {
            return View();
        }
        //Get
        public ActionResult Login()
        {
            /*string hashedPassword = HashingHelper.HashPassword("Admin@#$%123");
            Session["Password"]=hashedPassword;*/
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.User user)
        {
            using (DbClgEntities dbs = new DbClgEntities())
            {

                if (string.IsNullOrEmpty(user.Username) && string.IsNullOrEmpty(user.Password))
                {
                    // Both username and password are empty
                    ModelState.AddModelError("", "Please enter username and password.");
                    return View(user); // Return the login view with the error message
                }


                string hashedPassword = HashingHelper.HashPassword(user.Password);
                var usr = dbs.Users.Where(u => u.Username == user.Username && u.Password == hashedPassword).FirstOrDefault();

                if (usr != null)
                {
                    Session["Id"] = usr.Id.ToString();
                    Session["UserName"] = user.Username.ToString();
                    FormsAuthentication.SetAuthCookie(user.Username, false);

                    return RedirectToAction("home", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password Incorrect");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}