using CollegeManagement_VNR.Models;
using CollegeManagement_VNR;
using CollegeManagement_VNR.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace newProjectClg.Controllers
{

    public class StudentsController : Controller
    {

        DbClgEntities db = new DbClgEntities();
        // GET: Account
        public ActionResult Index()
        {
            List<User> students = db.Users.ToList();
            return View(students);
        }
        [HttpGet]
        public ActionResult RegisterStudent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterStudent(StudentRegVM obj)
        {
            if (string.IsNullOrEmpty(obj.Username) || string.IsNullOrEmpty(obj.Password))
            {

                ModelState.AddModelError("", "Please enter all details");
                return View(obj); // Return the login view with the error message
            }
            string hashedPassword = HashingHelper.HashPassword(obj.Password);
            var results = (from r in db.Students
                           where r.Department == obj.Department
                           orderby r.Roll_Number descending
                           let Roll_Number = r.Roll_Number.Substring(8, 10)
                           select new
                           {
                               RollNumber = Roll_Number
                           }
                        ).FirstOrDefault();
            int count;

            if (results == null || results.RollNumber == "")
            {
                count = 1;
            }
            else
            {
                count = Int32.Parse(results.RollNumber) + 1;
            }
            String RollNumber = "4VN23" + obj.Department + Convert.ToString(count).PadLeft(3, '0');

            //Veryfying whether the email already exists
            var isEmailAlreadyExists = db.Students.Any(x => x.Email == obj.Email);
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
            User u = new User();
            u.Username = obj.Username;
            u.Password = hashedPassword;
            u.Status = true;
            u.User_Type = 3;

            db.Users.Add(u);
            db.SaveChanges();

            Student student = new Student();
            student.Name = obj.Name;
            student.Gender = obj.Gender;
            student.Department = obj.Department;
            student.Roll_Number = RollNumber;
            student.Section = obj.Section;
            student.Email = obj.Email;
            student.Phone_Number = obj.Phone_Number;
            student.Student_Fee = obj.Student_Fee;
            student.User_Id = u.Id;

            db.Students.Add(student);
            db.SaveChanges();

            Address address = new Address();
            address.Door_Number = obj.Door_Number;
            address.Area = obj.Area;
            address.City = obj.City;
            address.Pincode = obj.Pincode;
            address.Student_Id = student.Id;

            db.Addresses.Add(address);
            db.SaveChanges();


            return RedirectToAction("home", "Home");
        }
    }
}