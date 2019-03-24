using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using SmartBreadcrumbs;
using WebStore.Entities.Entities;
using WebStore.Entities.ViewModels;
using WebStore.Entities.ViewModels.Admin;
using WebStore.Entities.ViewModels.Page;
using WebStore.Interfaces;
using WebStore.Interfaces.services;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;
        private readonly IConfiguration _configuration;

        public CatalogController(IProductData productData, IConfiguration configuration)
        {
            _productData = productData;
            _configuration = configuration;
        }


        [Breadcrumb("Shop")]
        public IActionResult Shop(int? sectionId, int? brandId, int page = 1)
        {
            var pageSize = int.Parse(_configuration["PageSize"]);

            var products = _productData.GetProducts(new ProductFilter
            {
                SectionId = sectionId,
                BrandId = brandId,
                Page = page,
                PageSize = pageSize
            });

            var model = new CatalogViewModel
            {
                BrandId = brandId,
                SectionId = sectionId,
                Products = products.Products.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price,
                    Brand = p.Brand != null ? new Brand() { Id = p.Brand.Id, Name = p.Brand.Name, Order = p.Brand.Order } : null
                }).OrderBy(p => p.Order).ToList(),
                PageViewModel = new ItemsPageViewModel
                {
                    PageSize = pageSize,
                    PageNumber = page,
                    TotalItems = products.TotalCount
                }
            };

            return View(model);
        }

        [Breadcrumb("Product details")]
        public IActionResult ProductDetails(int id)
        {
            var product = _productData.GetProductById(id);

            if (product == null)
                return NotFound();

            return View(new ProductViewModel
            {
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Order = product.Order,
                Price = product.Price,
                Brand = product != null ? new Brand() { Id = product.Brand.Id, Name = product.Brand.Name, Order = product.Brand.Order } : null
            });
        }
    }
}