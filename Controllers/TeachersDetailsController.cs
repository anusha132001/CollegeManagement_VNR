using CollegeManagement_VNR.Models;
using CollegeManagement_VNR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollegeManagement_VNR.Controllers;


namespace CollegeManagement_VNR.Controllers
{
    public class TeachersDetailsController : Controller
    {




        DbClgEntities db = new DbClgEntities();
        // GET: TeachersDetails
        public ActionResult Index()
        {
            List<User> students = db.Users.ToList();
            return View(students);
        }

        [HttpGet]
        public ActionResult RegisterTeacher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterTeacher(TeacherRegVM obj)
        {
            string Department = obj.Department;
            if (string.IsNullOrEmpty(obj.Username) || string.IsNullOrEmpty(obj.Password))
            {

                ModelState.AddModelError("", "Please enter all details");
                return View(obj); // Return the login view with the error message
            }
            string hashedPassword = HashingHelper.HashPassword(obj.Password);// why hashedPassword storing here(since no column in table)
            //Veryfying whether the email already exists
            var isEmailAlreadyExists = db.Teachers.Any(x => x.Email == obj.Email);
            if (isEmailAlreadyExists)
            {
                ModelState.AddModelError("Email", "User with this email already exists");
                return View(obj);
            }
            //Veryfying whether the username already exists
            var isUserNameAlreadyExists = db.Users.Any(x => x.Username == obj.Username);
            if (isUserNameAlreadyExists)
            {
                ModelState.AddModelError("UserName", "User with this username already exists");
                return View(obj);
            }

            //Employee_id generation
            var results = (from tt in db.Teachers



                           orderby tt.Employee_Id descending
                           let EmployeeId = tt.Employee_Id.Substring(3, 6)
                           select new
                           {
                               EmployeeId
                           }
                       ).FirstOrDefault();




            int count;



            if (results == null)
            {
                count = 1;
            }
            else
            {
                count = Int32.Parse(results.EmployeeId) + 1;
            }
            String Employ = "VNR" + Convert.ToString(count).PadLeft(3, '0');

            User u = new User();
            u.Username = obj.Username;
            u.Password = hashedPassword;
            u.Status = true;
            u.User_Type = 2;
            db.Users.Add(u);
            db.SaveChanges();

            Teacher t = new Teacher();
            t.Name = obj.Name;
            t.Gender = obj.Gender;
            t.Department = obj.Department;
            t.Employee_Id = Employ;
            t.Phone_Number = obj.Phone_Number;
            t.Email = obj.Email;
            t.User_Id = u.Id;
            db.Teachers.Add(t);
            db.SaveChanges();

            Address a = new Address();
            a.Door_Number = obj.Door_Number;
            a.Street = obj.Street;
            a.Area = obj.Area;
            a.City = obj.City;
            a.Pincode = obj.Pincode;
            a.Teacher_Id = t.Id;
            db.Addresses.Add(a);
            db.SaveChanges();



            return RedirectToAction("home", "Home");
        }
    }
}