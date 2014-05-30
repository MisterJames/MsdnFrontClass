using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FrontClass.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FrontClass.Filters
{
    /// <summary>
    /// Allows administrators and users who have a claim for the
    /// current course to access an attributed controller/action
    /// </summary>
    public class EnrolmentVerificationFilter : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // no active course
            if (MvcApplication.Classroom.CurrentModule == null)
                return false;
            
            // call base class to ensure user is signed in
            if (!base.AuthorizeCore(httpContext))
                return false;

            // the user must be an administrator...
            if (httpContext.User.IsInRole(MvcApplication.AdministratorRoleName))
                return true;

            // ...or have registered for the course
            return UserHasClaimForCourse(httpContext);

        }

        private static bool UserHasClaimForCourse(HttpContextBase httpContext)
        {
            var courseId = MvcApplication.Classroom.CurrentModule.CourseId;

            // check for the claim against the current course
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var userId = httpContext.User.Identity.GetUserId();

            var hasClaimForActiveCourse = userManager.GetClaims(userId)
                .Where(c => c.Type == MvcApplication.CourseRegistrationClaimUrn)
                .Any(c => c.Value == courseId.ToString(CultureInfo.InvariantCulture));

            return hasClaimForActiveCourse;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // force a login
                base.HandleUnauthorizedRequest(filterContext);
            }

            // they need to be enrolled in the course
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                                                                {
                                                                    { "controller", "Enrolment" }, 
                                                                    { "action", "Index" } 
                                                                }
                                                            );
        }
    }
}