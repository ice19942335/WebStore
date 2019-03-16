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
using WebStore.Interfaces.services;

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
            //throw new InvalidOperationException();
            var values = await _valuesService.GetAsync();
            return View(values);
        }


        //[DefaultBreadcrumb("Home")]
        //public IActionResult Index() => View();

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

        public IActionResult ErrorStatus(string id)
        {
            if (id == "404")
                return RedirectToAction("NotFound404");

            return Content($"Error status code: {id}");
        }

        public IActionResult ValuesServiceTest([FromServices] IValuesService valueServices) =>
            View(valueServices.Get());
    }
}