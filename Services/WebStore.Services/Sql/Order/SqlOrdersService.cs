using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Entities.Dto.Brand;
using WebStore.Entities.Dto.Order;
using WebStore.Entities.Dto.Product;
using WebStore.Entities.Entities;
using WebStore.Entities.Entities.Identity;
using WebStore.Interfaces.services;

namespace WebStore.Services.Sql.Order
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

        public IEnumerable<OrderDto> GetUserOrders(string userName)
        {
            return _context.Orders.Include("User").Include("OrderItems").Where(o
                => o.User.UserName.Equals(userName)).Select(o => new OrderDto()
                {
                    Id = o.Id,
                    Name = o.Name,
                    Address = o.Address,
                    Date = o.Date,
                    Phone = o.Phone,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDto()
                    {
                        Id = oi.Id,
                        Price = oi.Price,
                        Quantity = oi.Quantity
                    })
                }).ToList();
        }

        public OrderDto GetOrderById(int id)
        {
            var order = _context.Orders.Include("User").Include("OrderItems").FirstOrDefault(o => o.Id.Equals(id));

            if (order == null) return null;

            List<OrderItem> orderItems = new List<OrderItem>(order.OrderItems);

            List<OrderItemDto> orderItemsDto = new List<OrderItemDto>();

            foreach (var orderItem in orderItems)
            {
                orderItemsDto.Add(new OrderItemDto()
                {
                    Id = orderItem.Id,
                    Order = orderItem.Order,
                    Price = orderItem.Price,
                    Product = orderItem.Product,
                    Quantity = orderItem.Quantity
                });
            }

            return new OrderDto()
            {
                Id = order.Id,
                Address = order.Address,
                Date = order.Date,
                Name = order.Name,
                Phone = order.Phone,
                User = order.User,
                OrderItems = orderItemsDto
            };
        }

        public OrderDto CreateOrder(CreateOrderModel orderModel, string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            using (var transaction = _context.Database.BeginTransaction())
            {
                var order = new Entities.Entities.Order()
                {
                    Address = orderModel.OrderViewModel.Address,
                    Name = orderModel.OrderViewModel.Name,
                    Date = DateTime.Now,
                    Phone = orderModel.OrderViewModel.Phone,
                    User = user
                };

                _context.Orders.Add(order);

                foreach (var item in orderModel.OrderItems)
                {
                    var product = _context.Products.FirstOrDefault(p => p.Id.Equals(item.Id));

                    if (product == null)
                        throw new InvalidOperationException($"Продукт id:{item.Id} отсутвтует в базе");

                    var orderItem = new OrderItem()
                    {
                        Order = order,
                        Price = product.Price,
                        Quantity = item.Quantity,
                        Product = product
                    };

                    _context.OrderItems.Add(orderItem);
                }

                _context.SaveChanges();
                transaction.Commit();

                return GetOrderById(order.Id);
            }
        }

        #region ForAdminMenu

        public List<OrderDto> GetAllOrdersList()
        {
            List<Entities.Entities.Order> ordersList = new List<Entities.Entities.Order>(_context.Orders.Include("User").Include("OrderItems"));

            List<OrderItem> orderItems = new List<OrderItem>(_context.OrderItems);

            List<OrderDto> orderDtos = new List<OrderDto>();

            List<OrderItemDto> orderItemDtos = new List<OrderItemDto>();

            foreach (var orderItem in orderItems)
            {
                orderItemDtos.Add(new OrderItemDto()
                {
                    Id = orderItem.Quantity,
                    Price = orderItem.Price,
                    Quantity = orderItem.Quantity
                });
            }

            foreach (var order in ordersList)
            {
                orderDtos.Add(new OrderDto()
                {
                    Id = order.Id,
                    User = order.User,
                    Address = order.Address,
                    Date = order.Date,
                    Name = order.Name,
                    Phone = order.Phone,
                    OrderItems = orderItemDtos
                });
            }

            return orderDtos;
        }

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

        // This is method from admin Interface with return type-OrderDetailsViewModel
        /*
         public OrderDetailsViewModel GetOrderById(int id) => new OrderDetailsViewModel
         {
            Order = _context.Orders.Include("User").Include("OrderItems").FirstOrDefault(e => e.Id.Equals(id)),
            OrderItemsList = _context.OrderItems.Include("Product")
         };
        */

        #endregion
    }
}
