using System.Collections.Generic;
using WebStore.Entities.Entities;

namespace WebStore.Models.Admin
{
    public class OrderDetailsViewModel
    {
        public Entities.Entities.Order Order { get; set; }
        public IEnumerable<OrderItem> OrderItemsList { get; set; }
    }
}
