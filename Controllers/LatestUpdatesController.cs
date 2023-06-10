using System;
using System.Collections.Generic;
using CollegeManagement_VNR.ViewModels;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CollegeManagement_VNR.Models;


namespace newProjectClg.Controllers
{
    public class LatestUpdatesController : Controller
    {
        DbClgEntities db = new DbClgEntities();

        public DateTime? CreatedDate { get; set; }

        public ActionResult Update()
        {

            /*List<Student> student = new List<Student>();
            List<Teacher> teacher = new List<Teacher>();
            List<LatestUpdate> latestUpdate = new List<LatestUpdate>();*/

            var query = (from u in db.LatestUpdates
                         where u.Createdby == 13
            select new
                         LatestUpdateVM

                         {
                             Created_Date = u.Created_Date,
                             Information = u.Information,
                             Name = "Admin"

                         }).ToList()

            .Union

            (from u in db.LatestUpdates

             join t in db.Teachers on u.Createdby equals t.User_Id into teacherGroup

             from t in teacherGroup.DefaultIfEmpty().ToList()

             join s in db.Students on u.Createdby equals s.User_Id into studentGroup

             from s in studentGroup.DefaultIfEmpty().ToList()


             where u.Createdby != 13
            select new
             LatestUpdateVM
             {
                 Created_Date = u.Created_Date,

                 Information = u.Information,
                 Name = t.Name ?? s.Name

             });
            return View(query.ToList());

            /*return View(db.LatestUpdates.ToList());*/

        }


        [HttpPost]
        public ActionResult Update(LatestUpdate l)
        {

            if (string.IsNullOrEmpty(l.Information))
            {
                // Both username and password are empty
                ModelState.AddModelError("", "please enter text first");
                return RedirectToAction("Update"); // Return the login view with the error message
            }
            if (ModelState.IsValid)
            {

                LatestUpdate lu = new LatestUpdate();
                lu.Created_Date = DateTime.Now;
                lu.Information = l.Information;
                lu.Createdby = l.Createdby;
                db.LatestUpdates.Add(lu);
                db.SaveChanges();
                return RedirectToAction("Update");



            }
            return View(l);
        }
    }
}
