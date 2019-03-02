using System.ComponentModel.DataAnnotations;
using WebStore.Entities.Entities.Base;

namespace WebStore.Entities.Entities
{
    public class OrderItem : BaseEntity
    {
        public virtual Order Order { get; set; }

        [Required]
        public virtual Product Product { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
