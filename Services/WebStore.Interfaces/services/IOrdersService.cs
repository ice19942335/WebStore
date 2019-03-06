using System.Collections.Generic;
using WebStore.Entities.Dto.Order;

namespace WebStore.Interfaces.services
{
    public interface IOrdersService
    {
        IEnumerable<OrderDto> GetUserOrders(string userName);
        OrderDto GetOrderById(int id);
        OrderDto CreateOrder(CreateOrderModel orderModel, string userName);
    }
}
