using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebStore.Entities.Entities.Base;

namespace WebStore.Entities.Dto.Order
{
    public class OrderItemDto
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
        
    }
}
