using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Entities.Dto.Brand;
using WebStore.Entities.Dto.Page;
using WebStore.Entities.Dto.Product;
using WebStore.Entities.Dto.Section;
using WebStore.Entities.Entities;
using WebStore.Entities.ViewModels;
using WebStore.Interfaces.services;
using WebStore.Services.InMemory;

namespace WebStore.Services.Sql.Product

{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreContext _context;

        public SqlProductData(WebStoreContext context)
        {
            _context = context;
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public Section GetSectionById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Brand GetBrandById(int id)
        {
            throw new System.NotImplementedException();
        }
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------


        public IEnumerable<Section> GetSections()
        {
            return _context.Sections.ToList();
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _context.Brands.ToList();
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

        #region AdminFunctions

        public IQueryable<Entities.Entities.Product> GetAllProducts() => _context.Products;

        public ProductViewModel ProductCreate(ProductViewModel product)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var newProduct = new Entities.Entities.Product
                {
                    Name = product.Name,
                    Order = _context.Products.Max(e => e.Order) + 1,
                    SectionId = product.SectionId,
                    BrandId = product.BrandId,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price
                };

                _context.Products.Add(newProduct);

                _context.SaveChanges();
                transaction.Commit();

                product.Id = newProduct.Id;

                return product;
            }

        }

        public bool ProductEdit(ProductViewModel model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var product = _context.Products.FirstOrDefault(e => e.Id.Equals(model.Id));

                if (ReferenceEquals(product, null))
                    return false;

                product.Name = model.Name;
                product.ImageUrl = model.ImageUrl;
                product.Price = model.Price;
                product.SectionId = model.SectionId.Equals(null) ? null : model.SectionId;
                product.BrandId = model.BrandId.Equals(null) ? null : model.BrandId;

                _context.SaveChanges();
                transaction.Commit();
            }

            return true;
        }

        public ProductViewModel ProductDetails(int id)
        {
            var product = _context.Products.FirstOrDefault(e => e.Id.Equals(id));

            if (ReferenceEquals(product, null))
                return null;

            ProductViewModel model = new ProductViewModel()
            {
                Name = product.Name,
                Order = product.Order,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                SectionId = product.SectionId.Equals(null) ? null : product.SectionId,
                BrandId = product.BrandId.Equals(null) ? null : product.BrandId
            };

            return model;
        }

        public bool ProductDelete(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var product = _context.Products.First(e => e.Id.Equals(id));

                if (!ReferenceEquals(product, null))
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                _context.SaveChanges();
                transaction.Commit();
                return false;
            }
        }

        public bool FillListWithProductsDeleteLater()
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
                    return false;
                }

                transaction.Commit();

                return true;
            }
        }

        public ProductViewModel Create(ProductViewModel product)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var newProduct = new Entities.Entities.Product
                {
                    Name = product.Name,
                    Order = _context.Products.Max(e => e.Order) + 1,
                    SectionId = product.SectionId,
                    BrandId = product.BrandId,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price
                };

                _context.Products.Add(newProduct);

                _context.SaveChanges();
                transaction.Commit();

                product.Id = newProduct.Id;

                return product;
            }

        }

        #endregion
    }
}
