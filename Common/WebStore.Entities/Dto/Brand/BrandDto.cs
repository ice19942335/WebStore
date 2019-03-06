using WebStore.Entities.Entities.Base.Interfaces;

namespace WebStore.Entities.Dto.Brand
{
    public class BrandDto : INamedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
