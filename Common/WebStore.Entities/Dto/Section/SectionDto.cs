using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Entities.Entities.Base;
using WebStore.Entities.Entities.Base.Interfaces;

namespace WebStore.Entities.Dto.Section
{
    public class SectionDto : NamedEntity, IOrderedEntity
    {
        public int? ParentId { get; set; }

        public virtual Entities.Section ParentSection { get; set; }

        public int Order { get; set; }

        public virtual ICollection<Entities.Product> Products { get; set; }
    }
}
