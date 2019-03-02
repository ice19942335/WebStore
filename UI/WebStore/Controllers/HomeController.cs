using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs;
using WebStore.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{

    public class HomeController : Controller
    {
        private readonly IValuesService _valuesService;
        public HomeController(IValuesService valuesService)
        {
            _valuesService = valuesService;
        }


        [DefaultBreadcrumb("Home")]
        public async Task<IActionResult> Index()
        {
            var values = await _valuesService.GetAsync();
            return View(values);
        }

        //[Breadcrumb("Checkout", FromAction = "Home.Cart")]
        [Breadcrumb("Checkout")]
        public IActionResult Checkout() => View();

        [Breadcrumb("Contact us")] // сделать потомком [Breadcrumb("Contact us", FromAction = "Home.Checkout")]
        public IActionResult ContactUs() => View();

        [Breadcrumb("Blog page")]
        public IActionResult BlogSingle() => View();

        [Breadcrumb("Blog list")]
        public IActionResult Blog() => View();

        public IActionResult NotFound404() => View();
    }
}