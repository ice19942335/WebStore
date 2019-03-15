using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartBreadcrumbs;
using WebStore.Clients.Services;
using WebStore.Clients.Services.Users;
using WebStore.DAL.Context;
using WebStore.Entities.Entities.Identity;
using WebStore.Interfaces.services;
using WebStore.Logger;
using WebStore.Services.CookieCartService;
using WebStore.Services.InMemory;
using WebStore.Services.MiddleWare;
using WebStore.Services.Sql;
using WebStore.Services.Sql.Admin;

namespace WebStore
{
    public class Startup
    {
        /// <summary>
        /// Добавляем свойство для доступа к конфигурации
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// Добавляем новый конструктор, принимающий интерфейс IConfiguration
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // Добавляем сервисы, необходимые для mvc
            services.AddMvc();
            // Добавляем реализацию клиента
            services.AddTransient<IValuesService, ValuesClient>();
            services.AddTransient<IEmployeesData, EmployeesClient>();
            services.AddTransient<IProductData, ProductsClient>();
            services.AddTransient<IOrdersService, OrdersClient>();
            services.AddTransient<IUsersClient, UsersClient>();
            // Настройка Identity
            services.AddIdentity<User, IdentityRole>()
                .AddDefaultTokenProviders();
            services.AddTransient<IUserStore<User>, UsersClient>();
            services.AddTransient<IUserRoleStore<User>, UsersClient>();
            services.AddTransient<IUserClaimStore<User>, UsersClient>();
            services.AddTransient<IUserPasswordStore<User>, UsersClient>();
            services.AddTransient<IUserTwoFactorStore<User>, UsersClient>();
            services.AddTransient<IUserEmailStore<User>, UsersClient>();
            services.AddTransient<IUserPhoneNumberStore<User>, UsersClient>();
            services.AddTransient<IUserLoginStore<User>, UsersClient>();
            services.AddTransient<IUserLockoutStore<User>, UsersClient>();
            services.AddTransient<IRoleStore<IdentityRole>, RolesClient>();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan =
                    TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;
                // User settings
                options.User.RequireUniqueEmail = true;
            });
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                // If the LoginPath is not set here, ASP.NET Core will default to / Account / Login
                options.LoginPath = "/Account/Login";
                // If the LogoutPath is not set here, ASP.NET Core will default to/Account/Logout
                options.LogoutPath = "/Account/Logout";
                // If the AccessDeniedPath is not set here, ASP.NET Core will default to / Account / AccessDenied
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            // Настройки для корзины
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICartService, CookieCartService>();

            //BreadCrumbs
            services.UseBreadcrumbs(GetType().Assembly);
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider svp, ILoggerFactory log)
        {
            log.AddLog4Net();

            if (env.IsDevelopment())
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Home/Error");
            else
                app.UseExceptionHandler("/Home/Error");

            // Добавляем расширение для использования статических файлов, т.к. appsettings.json - это статический файл
            app.UseStaticFiles();

            app.UseWelcomePage("/welcome");

            app.UseAuthentication();

            app.UseStatusCodePagesWithRedirects("~/home/errorstatus/{0}");

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            // Добавляем обработку запросов в mvc-формате
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

