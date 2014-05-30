using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FrontClass.DAL;
using FrontClass.Models;


namespace FrontClass
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public const string AdministratorRoleName = "Administrator";
        public const string CourseRegistrationClaimUrn = "urn:FrontClass.CourseRegistration";
        public static Classroom Classroom = new Classroom();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<ApplicationDbContext>(
                new MigrateDatabaseToLatestVersion
                    <ApplicationDbContext, Models.AccountMigrations.Configuration>());

            Database.SetInitializer<FrontClassContext>(
                new MigrateDatabaseToLatestVersion
                    <FrontClassContext, Migrations.Configuration>());
            
        }

    }
}
