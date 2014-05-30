using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrontClass.DAL;
using FrontClass.Filters;
using FrontClass.Models;

namespace FrontClass.Controllers
{
    [EnrolmentVerificationFilter]
    public class ClassroomController : Controller
    {
        private FrontClassContext db = new FrontClassContext();

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult GetCurrentStep(int? stepId)
        {
            // step selected
            if (stepId.HasValue)
            {
                var step = db.Steps
                    .Include("Module")
                    .First(s => s.StepId == stepId);
                return PartialView("_CurrentStep", step);
            }

            // module exists, no step selected, load default
            if (MvcApplication.Classroom.CurrentStep != null)
            {
                return PartialView("_CurrentStep", MvcApplication.Classroom.CurrentStep);
            }

            // :o(
            return PartialView("_NoStep");
        }

    }
}