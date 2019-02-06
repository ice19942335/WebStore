using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Entities.Entities.Base;
using WebStore.Entities.Entities.Base.Interfaces;

namespace WebStore.Entities.Entities
{
    /// <inheritdoc cref="NamedEntity" />
    /// <summary>
    /// Сущность бренд
    /// </summary>
    [Table("Brands")]
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
