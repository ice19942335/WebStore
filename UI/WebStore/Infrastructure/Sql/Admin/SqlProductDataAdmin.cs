using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Entities.Entities;
using WebStore.Infrastructure.Implementations;
using WebStore.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Sql.Admin
{
    public class SqlProductDataAdmin : IProductDataAdmin
    {
        private readonly WebStoreContext _context;

        public SqlProductDataAdmin(WebStoreContext context)
        {
            _context = context;
        }

        public ProductViewModel Create(ProductViewModel product)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var newProduct = new Product
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

        public IQueryable<Product> GetAllProducts() => _context.Products;

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
    }
}
