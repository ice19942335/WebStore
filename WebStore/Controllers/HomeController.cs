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
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        [DefaultBreadcrumb("Home")]
        public IActionResult Index()
        {
            return View();
        }

        [Breadcrumb("Shop")]
        public IActionResult Shop()
        {
            return View();
        }

        [Breadcrumb("ProductDetails")]
        public IActionResult ProductDetails()
        {
            return View();
        }

        [Breadcrumb("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Breadcrumb("Checkout", FromAction = "Home.Cart")]
        public IActionResult Checkout()
        {
            return View();
        }

        [Breadcrumb("Contact us")] // сделать потомком [Breadcrumb("Contact us", FromAction = "Home.Checkout")]
        public IActionResult ContactUs()
        {
            return View();
        }

        [Breadcrumb("Cart")]
        public IActionResult Cart()
        {
            return View();
        }

        [Breadcrumb("Blog page")]
        public IActionResult BlogSingle()
        {
            return View();
        }

        [Breadcrumb("Blog list")]
        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult NotFound404()
        {
            return View();
        }

    }
}