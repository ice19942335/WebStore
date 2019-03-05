using WebStore.Entities.Dto.Brand;
using WebStore.Entities.Entities.Base.Interfaces;

namespace WebStore.Entities.Dto.Product
{
    public class ProductDto : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public BrandDto Brand { get; set; }
        public int? SectionId { get; set; }
        public int? BrandId { get; set; }
    }
}
