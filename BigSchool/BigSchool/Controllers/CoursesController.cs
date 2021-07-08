using BigSchool.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}