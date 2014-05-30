using System.Linq;
using System.Web.Mvc;
using FrontClass.DAL;

namespace FrontClass.Controllers
{
    [Authorize(Roles = MvcApplication.AdministratorRoleName)]
    public class InstructController : Controller
    {
        private FrontClassContext db = new FrontClassContext();

        public ActionResult Index(int moduleId)
        {
            var module = db.Modules
                .Include("Steps")
                .First(m => m.ModuleId == moduleId);

            return View(module);
        }
    }
}