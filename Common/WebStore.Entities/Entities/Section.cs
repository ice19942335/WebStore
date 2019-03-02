using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Entities.Entities.Base;
using WebStore.Entities.Entities.Base.Interfaces;

namespace WebStore.Entities.Entities
{
    /// <inheritdoc cref="NamedEntity" />
    /// <summary>
    /// Сущность секция
    /// </summary>
    [Table("Sections")]
    public class Section : NamedEntity, IOrderedEntity
    {
        /// <summary>
        /// Родительская секция (при наличии)
        /// </summary>
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual Section ParentSection { get; set; }

        public int Order { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
