using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Entities.Dto;
using WebStore.Entities.Dto.Page;
using WebStore.Entities.Dto.Product;
using WebStore.Entities.Entities;
using WebStore.Interfaces.services;

namespace WebStore.Clients.Services
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration configuration) : base (configuration)
        {
            ServiceAddress = "api/products";
        }

        protected sealed override String ServiceAddress { get; set; }

        public IEnumerable<Section> GetSections()
        {
            var url = $"{ServiceAddress}/sections";
            var result = Get<List<Section>>(url);
            return result;
        }

        public IEnumerable<Brand> GetBrands()
        {
            var url = $"{ServiceAddress}/brands";
            var resultt = Get<List<Brand>>(url);
            return resultt;
        }

        public PagedProductDto GetProducts(ProductFilter filter)
        {
            var url = $"{ServiceAddress}";
            var response = Post(url, filter);
            var result = response.Content.ReadAsAsync<PagedProductDto>().Result;
            return result;
        }

        public ProductDto GetProductById(int id)
        {
            var url = $"{ServiceAddress}/{id}";
            var result = Get<ProductDto>(url);
            return result;
        }

        public Section GetSectionById(int id)
        {
            throw new NotImplementedException();
        }

        public Brand GetBrandById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
