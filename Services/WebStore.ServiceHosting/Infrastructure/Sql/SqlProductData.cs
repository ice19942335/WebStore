using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Entities.Dto;
using WebStore.Entities.Dto.Brand;
using WebStore.Entities.Dto.Product;
using WebStore.Entities.Entities;
using WebStore.Interfaces.services;

namespace WebStore.ServiceHosting.Infrastructure.Sql

{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreContext _context;
        public SqlProductData(WebStoreContext context)
        {
            _context = context;
        }

        public IEnumerable<Section> GetSections()
        {
            return _context.Sections.ToList();
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _context.Brands.ToList();
        }

        public IEnumerable<ProductDto> GetProducts(ProductFilter filter)
        {
            var query =
                _context.Products.Include("Brand").Include("Section").AsQueryable();

            if (filter.BrandId.HasValue)
                query = query.Where(c => 
                    c.BrandId.HasValue &&
                    c.BrandId.Value.Equals(filter.BrandId.Value));

            if (filter.SectionId.HasValue)
                query = query.Where(c =>
                    c.SectionId.Equals(filter.SectionId.Value));

            return query.Select(p => new ProductDto()
            {
                Id = p.Id,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                SectionId = p.SectionId,
                BrandId = p.BrandId,
                Brand = p.BrandId.HasValue ? new BrandDto()
                {
                    Id = p.Brand.Id,
                    Name = p.Brand.Name
                } : null
            }).ToList();
        }

        public ProductDto GetProductById(int id)
        {
            var product = _context.Products.Include("Brand").Include("Section").FirstOrDefault(p => p.Id.Equals(id));
            if (product == null) return null;
            var dto = new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Order = product.Order,
                Price = product.Price,
                SectionId = product.SectionId,
                BrandId = product.BrandId
            };
            if (product.Brand != null)
                dto.Brand = new BrandDto()
                {
                    Id = product.Brand.Id,
                    Name = product.Brand.Name
                };
            return dto;
        }
    }
}
