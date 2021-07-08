using BigSchool.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace BigSchool.Controllers
{
    public class CoursesController : Controller
    {
        // GET: CoursesController
        public ActionResult create()
        {
            BigSchoolContext context = new BigSchoolContext();
            Course objcourse = new Course();
            objcourse.ListCategory = context.Categories.ToList();

            return View(objcourse);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create(Course objcourse)
        {

            BigSchoolContext context = new BigSchoolContext();

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>
                ().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            objcourse.LectureId = user.Id;

            context.Courses.Add(objcourse);
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Adttending(Course objcourse)
        {
            BigSchoolContext context = new BigSchoolContext();
            ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var listAttendances = context.Attendances.Where(p => p.Attendee == currentUser.Id).ToList();
            var courses = new List<Course>();
            foreach(Attendance temp in listAttendances)
            {
                Course objCourse = temp.Course;
                objCourse.Name = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(objCourse.LectureId).Name;
                courses.Add(objCourse);
            }
            return View(courses);
        }

        public ActionResult Mine()
        {
            ApplicationUser currentU = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            BigSchoolContext context = new BigSchoolContext();
            var courses = context.Courses.Where(c => c.LectureId == currentU.Id && c.DateTime > DateTime.Now).ToList();
            foreach(Course i in courses)
            {
                i.Name = currentU.Name;
            }
            return View(courses);
        }
    }
}