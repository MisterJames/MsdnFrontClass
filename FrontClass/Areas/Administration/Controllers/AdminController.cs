using System.Data.Entity;
using System.Web.Mvc;
using FrontClass.DAL;

namespace FrontClass.Areas.Administration.Controllers
{
    [Authorize(Roles = MvcApplication.AdministratorRoleName)]
    public class AdminController : Controller
    {
        private FrontClassContext db = new FrontClassContext();

        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Modules);

            return View(courses);
        }
    }
}