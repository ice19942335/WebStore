using WebStore.Entities.Entities.Base.Interfaces;

namespace WebStore.Entities.Entities.Base
{
    public class NamedEntity : BaseEntity, INamedEntity
    {
        public string Name { get; set; }
    }
}
