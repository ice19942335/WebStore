using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.Areas.Admin.Models;
using WebStore.DAL.Context;
using WebStore.DomainNew.Entities;
using WebStore.Entities.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Interfaces.Admin;

namespace WebStore.Infrastructure.Sql.Admin
{
    public class SqlOrdersServiceAdmin : IOrdersServiceAdmin
    {
        private readonly WebStoreContext _context;

        public SqlOrdersServiceAdmin(WebStoreContext webStoreContext)
        {
            _context = webStoreContext;
        }

        public IQueryable<Order> GetAllOrdersList() => _context.Orders.Include("User");

        public OrderDetailsViewModel GetOrderById(int id) => new OrderDetailsViewModel
        {
            Order = _context.Orders.Include("User").Include("OrderItems").FirstOrDefault(e => e.Id.Equals(id)),
            OrderItemsList = _context.OrderItems.Include("Product")
        };

        public bool DeleteOdrerById(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var order = _context.Orders.Single(e => e.Id.Equals(id));

                if (!ReferenceEquals(order, null))
                {
                    _context.Orders.Remove(order);
                    _context.SaveChanges();
                    transaction.Commit();
                    return true;
                }

                _context.SaveChanges();
                transaction.Commit();
                return false;
            }
        }
    }
}
