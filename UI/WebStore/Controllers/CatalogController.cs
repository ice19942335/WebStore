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

        public IActionResult Shop(int? sectionId, int? brandId, int page = 1)
        {
            var productsModel = GetProducts(sectionId, brandId, page, out var
                totalCount);
            var model = new CatalogViewModel()
            {
                BrandId = brandId,
                SectionId = sectionId,
                Products = productsModel,
                PageViewModel = new ProductsPageViewModel
                {
                    PageSize = int.Parse(_configuration["PageSize"]),
                    PageNumber = page,
                    TotalItems = totalCount
                }
            };
            return View(model);
        }

        public IActionResult GetFilteredItems(int? sectionId, int? brandId, int page = 1)
        {
            var productsModel = GetProducts(sectionId, brandId, page, out var totalCount);
            return PartialView("_ProductItems", productsModel);
        }

        private IEnumerable<ProductViewModel> GetProducts(int? sectionId, int? brandId, int page, out int totalCount)
        {
            var products = _productData.GetProducts(new ProductFilter
            {
                BrandId = brandId,
                SectionId = sectionId,
                Page = page,
                PageSize = int.Parse(_configuration["PageSize"])
            });

            totalCount = products.TotalCount;

            return products.Products.Select(p => new ProductViewModel()
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                Brand = p.Brand != null ? new Brand() { Id = p.Brand.Id, Order = p.Brand.Order, Name = p.Brand.Name } : null
            }).ToList();
        }

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
                Brand = product.Brand != null ? new Brand(){ Id = product.Brand.Id, Order = product.Brand.Order, Name = product.Brand.Name} : null
            });
        }
    }
}