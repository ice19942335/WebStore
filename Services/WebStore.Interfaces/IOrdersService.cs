using System.Collections.Generic;
using WebStore.Entities.Entities;
using WebStore.Models.Cart;
using WebStore.Models.Order;

namespace WebStore.Interfaces
{
    public interface IOrdersService
    {
        IEnumerable<Order> GetUserOrders(string userName);

        Order GetOrderById(int id);

        Order CreateOrder(OrderViewModel orderModel, CartViewModel transformCart, string userName);

    }
}
