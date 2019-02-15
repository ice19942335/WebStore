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
                    Order = product.Order.Equals(null) ? 1 : product.Order,
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

        public ProductViewModel Edit(ProductViewModel product)
        {
            throw new NotImplementedException();
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
