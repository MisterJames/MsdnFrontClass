using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using FrontClass.DAL;
using FrontClass.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FrontClass.Controllers
{
    [Authorize]
    public class EnrolmentController : Controller
    {
        private FrontClassContext db = new FrontClassContext();

        // GET: Enrolment
        public ActionResult Index()
        {
            var enrolment = BuildCourseEnrolmentViewModel();

            return View(enrolment);
        }

        private CourseEnrolmentViewModel BuildCourseEnrolmentViewModel()
        {
var context = new ApplicationDbContext();
var userStore = new UserStore<ApplicationUser>(context);
var userManager = new UserManager<ApplicationUser>(userStore);

var userId = User.Identity.GetUserId();
var claims = userManager.GetClaims(userId)
    .Where(c => c.Type == MvcApplication.CourseRegistrationClaimUrn)
    .ToList();

            var courses = db.Courses;

            var enrolment = new CourseEnrolmentViewModel();

            foreach (var course in courses)
            {
                var enrolled = claims.Any(c => c.Value == course.CourseId.ToString(CultureInfo.InvariantCulture));
                enrolment.Courses.Add(course, enrolled);
            }
            return enrolment;
        }


        [HttpPost]
        public ActionResult Index(EnrolInCourseViewModel enrol)
        {
            // verify entry code for course
            if (!db.Courses.Any(c => c.CourseId == enrol.CourseId && c.EntryCode == enrol.EntryCode))
            {
                TempData["CourseRegistrationError"] = "Could not find a course for the entry code provided.";
                return RedirectToAction("Index");
            }
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            
            var userId = User.Identity.GetUserId();
            var courseClaim = new Claim(MvcApplication.CourseRegistrationClaimUrn, enrol.CourseId.ToString(CultureInfo.InvariantCulture));
            userManager.AddClaim(userId, courseClaim);

            return RedirectToAction("Index");
        }

    }
}