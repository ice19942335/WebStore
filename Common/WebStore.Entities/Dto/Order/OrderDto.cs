using System;
using System.Collections.Generic;
using WebStore.Entities.Entities.Base;

namespace WebStore.Entities.Dto.Order
{
    public class OrderDto : NamedEntity
    {
        public virtual Entities.Identity.User User { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<OrderItemDto> OrderItems { get; set; }
    }

}
