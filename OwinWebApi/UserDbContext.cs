using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using OwinWebApi.Models;
using OwinWebApi.Services;

namespace OwinWebApi
{
    public class UserDbContext : DbContext
    {
        public UserDbContext() : base("OwinConnection")
        {
        }

        static UserDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<UserClaim> Claims { get; set; }

    }

    public class DbInitializer : DropCreateDatabaseIfModelChanges<UserDbContext>
    {

        protected async override void Seed(UserDbContext context)
        {
            context.Companies.Add(new Company { Name = "Microsoft" });
            context.Companies.Add(new Company { Name = "Apple" });
            context.Companies.Add(new Company { Name = "Google" });
            context.SaveChanges();

            // Set up two initial users with different role claims:
            var john = new User { Email = "john@example.com" };
            var jimi = new User { Email = "jimi@Example.com" };

            john.Claims.Add(new UserClaim
            {
                ClaimType = ClaimTypes.Name,
                UserId = john.Id,
                ClaimValue = john.Email
            });
            john.Claims.Add(new UserClaim
            {
                ClaimType = ClaimTypes.Role,
                UserId = john.Id,
                ClaimValue = "Admin"
            });

            jimi.Claims.Add(new UserClaim
            {
                ClaimType = ClaimTypes.Name,
                UserId = jimi.Id,
                ClaimValue = jimi.Email
            });
            jimi.Claims.Add(new UserClaim
            {
                ClaimType = ClaimTypes.Role,
                UserId = john.Id,
                ClaimValue = "User"
            });

            var store = new MyUserStore(context);
            store.AddUser(john, "JohnsPassword");
            store.AddUser(jimi, "JimisPassword");
        }

    }
}
