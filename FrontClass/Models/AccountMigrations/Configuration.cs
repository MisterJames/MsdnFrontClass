using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FrontClass.Models.AccountMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FrontClass.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Models\AccountMigrations";
        }

        protected override void Seed(FrontClass.Models.ApplicationDbContext context)
        {

            if (!context.Roles.Any(r => r.Name == FrontClass.MvcApplication.AdministratorRoleName))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var identityRole = new IdentityRole {Name = FrontClass.MvcApplication.AdministratorRoleName};

                roleManager.Create(identityRole);
            }

            var instructorName = "instructor@contonso.com";
            if (!context.Users.Any(u => u.UserName == instructorName))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var applicationUser = new ApplicationUser { UserName = instructorName };

                userManager.Create(applicationUser, "admin_2014");
                userManager.AddToRole(applicationUser.Id, FrontClass.MvcApplication.AdministratorRoleName);
            }
        }
    }
}
