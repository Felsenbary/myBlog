using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using myBlog.Models;
using System.Data.Entity.Migrations;
using System.Linq;
namespace myBlog.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            if (!context.Users.Any(u => u.Email == "felsenbary@outlook.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "felsenbary@outlook.com",
                    Email = "felsenbary@outlook.com",
                    DisplayName = "Fred"
                }, "Abc&123!");
            }
            if (!context.Users.Any(u => u.Email == "coderfoundry@outlook.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "coderfoundry@outlook.com",
                    Email = "coderfoundry@outlook.com",
                    DisplayName = "Fredy"
                }, "Abc&123!");
            }

            var userId = userManager.FindByEmail("felsenbary@outlook.com").Id;
            userManager.AddToRole(userId, "Admin");
            var user_Id = userManager.FindByEmail("coderfoundry@outlook.com").Id;
            userManager.AddToRole(user_Id, "Moderator");
        }
    }
}