using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.DomainNew.Entities;
using WebStore.Entities.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models.Cart;
using WebStore.Models.Order;

namespace WebStore.Infrastructure.Sql
{
    public class SqlOrdersService : IOrdersService
    {
        private readonly WebStoreContext _context;
        private readonly UserManager<User> _userManager;

        public SqlOrdersService(WebStoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IEnumerable<Order> GetUserOrders(string userName) => _context.Orders.Include("User").Include("OrderItems").Where(o => o.User.UserName.Equals(userName)).ToList();

        public Order GetOrderById(int id) =>_context.Orders.Include("OrderItems").FirstOrDefault(o => o.Id.Equals(id));
        
        public Order CreateOrder(OrderViewModel orderModel, CartViewModel transformCart, string userName)
        {

            User user = new User();
            try
            {
                user = _userManager.FindByNameAsync(userName).Result;
                if (user.Email.Equals(null))
                    return new Order();
            }
            catch (Exception e)
            {
                    return new Order();
            }
            
            

            using (var transaction = _context.Database.BeginTransaction())
            {
                var order = new Order
                {
                    Address = orderModel.Address,
                    Name = orderModel.Name,
                    Date = DateTime.Now,
                    Phone = orderModel.Phone,
                    User = user
                };

                _context.Orders.Add(order);

                foreach (var item in transformCart.Items)
                {
                    var productVm = item.Key;
                    var product = _context.Products.FirstOrDefault(p => p.Id.Equals(productVm.Id));

                    if (product == null)
                        throw new InvalidOperationException("Product not found in database");

                    var orderItem = new OrderItem
                    {
                        Order = order,
                        Price = product.Price,
                        Quantity = item.Value,
                        Product = product
                    };
                    _context.OrderItems.Add(orderItem);
                }

                _context.SaveChanges();
                transaction.Commit();
                return order;
            }
        }

    }
}
