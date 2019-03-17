using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebStore.Clients.Services;
using WebStore.DAL.Context;
using WebStore.Entities.Entities.Identity;
using WebStore.Interfaces;
using WebStore.Interfaces.services;
using WebStore.Services.Cart;
using WebStore.Services.CookieCartService;
using WebStore.Services.InMemory;
using WebStore.Services.Sql;
using WebStore.Services.Sql.Admin;
using WebStore.Services.Sql.Order;
using WebStore.Services.Sql.Product;

namespace WebStore.ServiceHosting
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<WebStoreContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<WebStoreContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();

            services.AddScoped<IProductData, SqlProductData>();
            services.AddScoped<IOrdersService, SqlOrdersService>();

            //Admin
            services.AddScoped<IProductDataAdmin, SqlProductDataAdmin>();
            services.AddScoped<IOrdersServiceAdmin, SqlOrdersServiceAdmin>();

            // Настройки для корзины
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // Добавляем сервис доступа к контексту http-запроса для обеспечения возможности использования нашего сервиса работы с корзиной покупателя

            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartStore, CookiesCartStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts(); //see https://aka.ms/aspnetcore-hsts
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
