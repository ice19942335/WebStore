using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Entities.Dto.Order;
using WebStore.Interfaces.services;

namespace WebStore.Clients.Services
{
    public class OrdersClient : BaseClient, IOrdersService
    {
        public OrdersClient(IConfiguration configuration) : base(configuration)
        {
            ServiceAddress = "api/orders";
        }
        protected sealed override string ServiceAddress { get; set; }
        public IEnumerable<OrderDto> GetUserOrders(string userName)
        {
            var url = $"{ServiceAddress}/user/{userName}";
            var result = Get<List<OrderDto>>(url);
            return result;
        }
        public OrderDto GetOrderById(int id)
        {
            var url = $"{ServiceAddress}/{id}";
            var result = Get<OrderDto>(url);
            return result;
        }
        public OrderDto CreateOrder(CreateOrderModel orderModel, string
            userName)
        {
            var url = $"{ServiceAddress}/{userName}";
            var response = Post(url, orderModel);
            var result = response.Content.ReadAsAsync<OrderDto>().Result;
            return result;
        }
    }
}
