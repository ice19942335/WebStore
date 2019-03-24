using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Entities.Dto.Brand;
using WebStore.Entities.Dto.Page;
using WebStore.Entities.Dto.Product;
using WebStore.Entities.Dto.Section;
using WebStore.Entities.Entities;
using WebStore.Interfaces.services;

namespace WebStore.Services.Sql.Product

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

        public Section GetSectionById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _context.Brands.ToList();
        }

        public Brand GetBrandById(int id)
        {
            throw new System.NotImplementedException();
        }

        public PagedProductDto GetProducts(ProductFilter filter)
        {
            var query =  _context.Products.Include("Brand").Include("Section").AsQueryable();
            if (filter.BrandId.HasValue)
                query = query.Where(c => c.BrandId.HasValue &&
                                         c.BrandId.Value.Equals(filter.BrandId.Value));
            if (filter.SectionId.HasValue)
                query = query.Where(c =>
                    c.SectionId.Equals(filter.SectionId.Value));
            var model = new PagedProductDto
            {
                TotalCount = query.Count()
            };
            if (filter.PageSize.HasValue)
            {
                model.Products = query.OrderBy(c => c.Order).
                    Skip((filter.Page - 1) * filter.PageSize.Value).
                    Take(filter.PageSize.Value)
                    .Select(p =>
                        new ProductDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Order = p.Order,
                            Price = p.Price,
                            ImageUrl = p.ImageUrl,
                            Brand = p.BrandId.HasValue ? new BrandDto()
                            {
                                Id = p.Brand.Id,
                                Name = p.Brand.Name
                            } : null,
                            Section = new SectionDto()
                            {
                                Id = p.Section.Id,
                                Name = p.Section.Name
                            }
                        }).ToList();
            }
            else
            {
                model.Products = query.OrderBy(c => c.Order).Select(p =>
                    new ProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Order = p.Order,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl,
                        Brand = p.BrandId.HasValue ? new BrandDto()
                        {
                            Id =
                                p.Brand.Id,
                            Name = p.Brand.Name
                        } : null,
                        Section = new SectionDto()
                        {
                            Id = p.Section.Id,
                            Name = p.Section.Name
                        }
                    }).ToList();
            }
            return model;
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
                    Name = product.Brand.Name,
                    Order = product.Brand.Order
                };

            return dto;
        }
    }
}
