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
using WebStore.Infrastructure.Enums;
using WebStore.Infrastructure.Implementations;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IProductData _productData;
        private readonly WebStoreContext _context;
        private readonly IProductDataAdmin _productDataAdmin;
        private readonly IOrdersServiceAdmin _ordersServiceAdmin;

        public HomeController(IProductData productData, WebStoreContext webStoreContext, IProductDataAdmin productDataAdmin, IOrdersServiceAdmin ordersServiceAdmin)
        {
            _productData = productData;
            _context = webStoreContext;
            _productDataAdmin = productDataAdmin;
            _ordersServiceAdmin = ordersServiceAdmin;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProductList(int page = 1)
        {
            int pageSize = 5;
            IQueryable<Product> productsList = _context.Products;

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

        public async Task<IActionResult> OrdersList(DayOfWeek? day, string name, int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 5;
            IQueryable<Order> ordersList = _context.Orders;

            if (day != null)
                ordersList = ordersList.Where(e => e.Date.DayOfWeek.Equals(day));

            if (!string.IsNullOrEmpty(name))
                ordersList = ordersList.Where(e => e.Name.Contains(name));

            switch (sortOrder)
            {
                case SortState.DateDesc:
                    ordersList = ordersList.OrderByDescending(e => e.Date);
                    break;
                case SortState.NameDesc:
                    ordersList = ordersList.OrderByDescending(e => e.Name);
                    break;
                case SortState.NameAsc:
                    ordersList = ordersList.OrderBy(e => e.Name);
                    break;
                default:
                    ordersList = ordersList.OrderBy(e => e.Date);
                    break;
            }

            var count = await ordersList.CountAsync();
            var items = await ordersList.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            OrdersListViewModel viewModel = new OrdersListViewModel
            {
                PageViewModel = pageViewModel,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(day, name),
                OrdersList = items
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ProductEdit(int? id)
        {
            ProductViewModel model;
            if (id.HasValue)
            {
                model = _productData.GetProductById(id.Value);
                if (ReferenceEquals(model, null))
                    return NotFound();
            }
            else
            {
                model = new ProductViewModel();
            }

            return View("ProductEdit", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ProductEdit(ProductViewModel model)
        {
            if (model.Id > 0)
            {
                if (_productDataAdmin.ProductEdit(model))
                    return RedirectToAction(nameof(ProductList));
            }
            else
            {
                if (ModelState.IsValid)
                    AddNewProduct(model);
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(ProductList));
            else
                return View("ProductEdit", model);
        }

        public void AddNewProduct(ProductViewModel dbItemProduct)
        {
            _productDataAdmin.Create(dbItemProduct);
        }

        public IActionResult ProductDetails(int id)
        {
            if (!_productDataAdmin.ProductDetails(id).Equals(null))
                return View(_productDataAdmin.ProductDetails(id));
            else
                return RedirectToAction(nameof(ProductList), _productData.GetProductById(id));
        }

        public IActionResult ProductDelete(int id)
        {
            if (_productDataAdmin.ProductDelete(id))
                return RedirectToAction(nameof(ProductList));

            return View("PleaseTryAgain");
        }

        public IActionResult FillDdFromProducts()
        {
            var memoryData = new InMemoryProductData();

            using (var transaction = _context.Database.BeginTransaction())
            {
                foreach (var product in memoryData.Products)
                    _context.Products.Add(product);

                try
                {
                    _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Products] ON");
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Products] OFF");
                }
                catch (Exception)
                {
                    return Content("List have to be empty, because there is test data with Id duplicates");
                }
                
                transaction.Commit();
            }
            

            return RedirectToAction(nameof(ProductList));
        }
    }

}