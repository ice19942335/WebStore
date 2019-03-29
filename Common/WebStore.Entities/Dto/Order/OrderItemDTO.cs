using System.ComponentModel.DataAnnotations;
using WebStore.Entities.Entities.Base;

namespace WebStore.Entities.Dto.Order
{
    public class OrderItemDto : BaseEntity
    {
        public virtual Entities.Order Order { get; set; }

        [Required]
        public virtual Entities.Product Product { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}