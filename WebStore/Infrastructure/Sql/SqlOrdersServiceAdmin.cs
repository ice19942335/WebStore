using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebStore.Areas.Admin.Models;
using WebStore.DAL.Context;
using WebStore.DomainNew.Entities;
using WebStore.Entities.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Sql
{
    public class SqlOrdersServiceAdmin : IOrdersServiceAdmin
    {
        private readonly WebStoreContext _context;
        private readonly UserManager<User> _userManager;

        public SqlOrdersServiceAdmin(WebStoreContext webStoreContext, UserManager<User> userManager)
        {
            _context = webStoreContext;
            _userManager = userManager;
        }

        public IEnumerable<Order> GetAllOrdersList() => _context.Orders.ToList();

        public OrderDetailsViewModel GetOrderById(int id)
        {
            //var order = _context.Orders.Include("User").Include("OrderItems").FirstOrDefault(e => e.Id.Equals(id));

            return new OrderDetailsViewModel
            {
                Order = _context.Orders.Include("User").Include("OrderItems").FirstOrDefault(e => e.Id.Equals(id)),
                OrderItemsList = _context.OrderItems.Include("Product")
            };
        }
    }
}
