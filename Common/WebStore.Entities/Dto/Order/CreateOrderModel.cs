using System.Collections.Generic;
using WebStore.Entities.ViewModels.Order;

namespace WebStore.Entities.Dto.Order
{
    public class CreateOrderModel
    {
        public OrderViewModel OrderViewModel { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
    }
}