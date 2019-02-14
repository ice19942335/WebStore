using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebStore.Areas.Admin.Models;
using WebStore.DAL.Context;
using WebStore.DomainNew.Entities;
using WebStore.Entities.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IProductData _productData;
        private readonly WebStoreContext _webStoreContext;

        public HomeController(IProductData productData, WebStoreContext webStoreContext)
        {
            _productData = productData;
            _webStoreContext = webStoreContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProductList(int page = 1)
        {
            int pageSize = 3;
            IQueryable<Product> productsList = _webStoreContext.Products;

            var count = await productsList.CountAsync();
            var items = await productsList.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            
            ProductListViewModel viewModel = new ProductListViewModel
            {
                PageViewModel = pageViewModel,
                Products = items
            };

            return View(viewModel);
        }

        public 

    }

}