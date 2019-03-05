using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Entities.Dto;
using WebStore.Entities.Dto.Product;
using WebStore.Entities.Entities;
using WebStore.Interfaces.services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductData _productData;

        public ProductsApiController(IProductData productData)
        {
            _productData = productData;
        }

        [HttpGet("sections")]
        public IEnumerable<Section> GetSections()
        {
            return _productData.GetSections();
        }

        [HttpGet("brands")]
        public IEnumerable<Brand> GetBrands()
        {
            return _productData.GetBrands();
        }

        [HttpPost]
        [ActionName("Post")]
        public IEnumerable<ProductDto> GetProducts([FromBody] ProductFilter filter)
        {
            return _productData.GetProducts(filter);
        }

        [HttpGet("{id}")]
        [ActionName("Get")]
        public ProductDto GetProductById(int id)
        {
            return _productData.GetProductById(id);
        }
    }
}