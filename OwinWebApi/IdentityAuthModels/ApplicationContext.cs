using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OwinWebApi.Services;

namespace OwinWebApi.Models
{
    //Classic identity realization
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
            : base("MyDatabase")
        {

        }

        static ApplicationDbContext()
        {
            Database.SetInitializer(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<Company> Companies { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<UserClaim> Claims { get; set; }
    }

    public class ApplicationDbInitializer
        : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected async override void Seed(ApplicationDbContext context)
        {
            context.Companies.Add(new Company { Name = "Microsoft" });
            context.Companies.Add(new Company { Name = "Apple" });
            context.Companies.Add(new Company { Name = "Google" });
            context.SaveChanges();

            // Set up two initial users with different role claims:
            var john = new ApplicationUser
            {
                Email = "john@example.com",
                UserName = "john@example.com"
            };
            var jimi = new ApplicationUser
            {
                Email = "jimi@Example.com",
                UserName = "jimi@example.com"
            };

            // Introducing...the UserManager:
            var manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            var result1 = manager.Create(john, "JohnsPassword");
            var result2 = manager.Create(jimi, "JimisPassword");

            // Add claims for user #1:
             manager.AddClaim(john.Id,
                new Claim(ClaimTypes.Name, "john@example.com"));

            manager.AddClaim(john.Id,
                new Claim(ClaimTypes.Role, "Admin"));

            // Add claims for User #2:
             manager.AddClaim(jimi.Id,
                new Claim(ClaimTypes.Name, "jimi@example.com"));

             manager.AddClaim(jimi.Id,
                new Claim(ClaimTypes.Role, "User"));
        }
    }
}
