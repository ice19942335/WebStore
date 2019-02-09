using System;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebStore.DAL;
using WebStore.DAL.Context;
using WebStore.DomainNew.Entities;

namespace WebStore.Data
{
    public class DbInitializer
    {
        public static void Initialize(WebStoreContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any()) return;

            using (var transaction = context.Database.BeginTransaction())
            {
                foreach (var section in TestData.Sections) context.Sections.Add(section);

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Sections] ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                transaction.Commit();
            }

            using (var transaction = context.Database.BeginTransaction())
            {
                foreach (var brand in TestData.Brands) context.Brands.Add(brand);

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Brands] ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                transaction.Commit();
            }

            using (var transaction = context.Database.BeginTransaction())
            {
                foreach (var product in TestData.Products) context.Products.Add(product);

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Products] ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Products] OFF");

                transaction.Commit();
            }
        }

        public static void InitializeIdentity(IServiceProvider service)
        {
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            const string roleNameUser = "User";
            if (!roleManager.RoleExistsAsync(roleNameUser).Result)
                roleManager.CreateAsync(new IdentityRole(roleNameUser)).Wait();

            const string roleNameAdmin = "Admin";
            if (!roleManager.RoleExistsAsync(roleNameAdmin).Result)
                roleManager.CreateAsync(new IdentityRole(roleNameAdmin)).Wait();

            var userManager = service.GetService<UserManager<User>>();
            var users = service.GetService<IUserStore<User>>();

            const string userNameAdmin = "Admin";
            if (users.FindByNameAsync(userNameAdmin, CancellationToken.None).Result == null)
            {
                var admin = new User
                {
                    UserName = userNameAdmin,
                    Email = $"{userNameAdmin.ToLower()}@server.com"
                };

                if (userManager.CreateAsync(admin, "AdminPassword@123").Result.Succeeded)
                    userManager.AddToRoleAsync(admin, roleNameAdmin).Wait();
            }
        }
    }
}