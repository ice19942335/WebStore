using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Entities.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Sql
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

        public bool Edit(ProductViewModel model)
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

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Details(int id)
        {
            throw new NotImplementedException();
        }
    }
}
