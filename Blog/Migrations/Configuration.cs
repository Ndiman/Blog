using Blog.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Blog.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<Blog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Blog.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var RoleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            
            if(!context.Roles.Any(r => r.Name == "Admin"))
            {
                RoleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                RoleManager.Create(new IdentityRole { Name = "Moderator" });
            }


            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "NicoleIman@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "NicoleIman@Mailinator.com",
                    Email = "NicoleIman@Mailinator.com",
                    FirstName = "Nicole",
                    LastName = "Iman",
                    DisplayName = "NDIman",
                }, "Abc&123!");
            }

            if (!context.Users.Any(u => u.Email == "JaneDoe@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "JaneDoe@Mailinator.com",
                    Email = "JaneDoe@Mailinator.com",
                    FirstName = "Jane",
                    LastName = "Doe",
                    DisplayName = "JD",
                }, "Abc&123!");
            }

            var userId = userManager.FindByEmail("NicoleIman@Mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            var modId = userManager.FindByEmail("JaneDoe@Mailinator.com").Id;
            userManager.AddToRole(modId, "Moderator");
        }
    }
}
