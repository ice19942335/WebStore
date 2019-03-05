using System.Collections.Generic;
using WebStore.Entities.Entities;

namespace WebStore.Entities.ViewModels.Admin
{
    public class OrderDetailsViewModel
    {
        public Entities.Order Order { get; set; }
        public IEnumerable<OrderItem> OrderItemsList { get; set; }
    }
}
