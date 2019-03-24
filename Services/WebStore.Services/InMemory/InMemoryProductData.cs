using System.Collections.Generic;
using System.Linq;
using WebStore.Entities.Dto.Brand;
using WebStore.Entities.Dto.Page;
using WebStore.Entities.Dto.Product;
using WebStore.Entities.Dto.Section;
using WebStore.Entities.Entities;
using WebStore.Interfaces.services;

namespace WebStore.Services.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public List<Section> Section { get; }
        public List<Brand> Brand { get; }
        public List<Product> Products { get; }


        public InMemoryProductData()
        {
            Section = new List<Section>
            {
                new Section {Id = 1, Name = "Sportswear - 1", Order = 0, ParentId = null},
                new Section {Id = 2, Name = "Nike - 2", Order = 0, ParentId = 1},
                new Section {Id = 3, Name = "Under Armour - 3", Order = 1, ParentId = 1},
                new Section {Id = 4, Name = "Adidas - 4", Order = 2, ParentId = 1},
                new Section {Id = 5, Name = "Puma - 5", Order = 3, ParentId = 1},
                new Section {Id = 6, Name = "ASICS - 6", Order = 4, ParentId = 1},
                new Section {Id = 7, Name = "Mens - 7", Order = 1, ParentId = null},
                new Section {Id = 8, Name = "Fendi - 8", Order = 0, ParentId = 7},
                new Section {Id = 9, Name = "Guess - 9", Order = 1, ParentId = 7},
                new Section {Id = 10, Name = "Valentino - 10", Order = 2, ParentId = 7},
                new Section {Id = 11, Name = "Dior - 11", Order = 3, ParentId = 7},
                new Section {Id = 12, Name = "Versace - 12", Order = 4, ParentId = 7},
                new Section {Id = 13, Name = "Armani - 13", Order = 5, ParentId = 7},
                new Section {Id = 14, Name = "Prada - 14", Order = 6, ParentId = 7},
                new Section {Id = 15, Name = "Dolce and Gabbana - 15", Order = 7, ParentId = 7},
                new Section {Id = 16, Name = "Chanel - 16", Order = 8, ParentId = 7},
                new Section {Id = 17, Name = "Gucci - 17", Order = 1, ParentId = 7},
                new Section {Id = 18, Name = "Womens - 18", Order = 2, ParentId = null},
                new Section {Id = 19, Name = "Fendi - 19", Order = 0, ParentId = 18},
                new Section {Id = 20, Name = "Guess - 20", Order = 1, ParentId = 18},
                new Section {Id = 21, Name = "Valentino - 21", Order = 2, ParentId = 18},
                new Section {Id = 22, Name = "Dior - 22", Order = 3, ParentId = 18},
                new Section {Id = 23, Name = "Versace - 23", Order = 4, ParentId = 18},
                new Section {Id = 24, Name = "Kids - 24", Order = 3, ParentId = null},
                new Section {Id = 25, Name = "Fashion - 25", Order = 4, ParentId = null},
                new Section {Id = 26, Name = "Households - 26", Order = 5, ParentId = null},
                new Section {Id = 27, Name = "Interiors - 27", Order = 6, ParentId = null},
                new Section {Id = 28, Name = "Clothing - 28", Order = 7, ParentId = null},
                new Section {Id = 29, Name = "Bags - 29", Order = 8, ParentId = null},
                new Section {Id = 30, Name = "Shoes - 30", Order = 9, ParentId = null}
            };

            Brand = new List<Brand>
            {
                new Brand {Id = 1, Name = "Acne - 1", Order = 0},
                new Brand {Id = 2, Name = "Grüne Erde - 2", Order = 1},
                new Brand {Id = 3, Name = "Albiro - 3", Order = 2},
                new Brand {Id = 4, Name = "Ronhill - 4", Order = 3},
                new Brand {Id = 5, Name = "Oddmolly - 5", Order = 4},
                new Brand {Id = 6, Name = "Boudestijn - 6", Order = 5},
                new Brand {Id = 7, Name = "Rösch creative culture - 7", Order = 6}
            };

            Products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Product-1",
                    Price = 1025,
                    ImageUrl = "product1.jpg",
                    Order = 0,
                    SectionId = 1,
                    BrandId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Product-2",
                    Price = 1025,
                    ImageUrl = "product2.jpg",
                    Order = 1,
                    SectionId = 2,
                    BrandId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Product-3",
                    Price = 1025,
                    ImageUrl = "product3.jpg",
                    Order = 2,
                    SectionId = 3,
                    BrandId = 2
                },
                new Product
                {
                    Id = 4,
                    Name = "Product-4",
                    Price = 1025,
                    ImageUrl = "product4.jpg",
                    Order = 3,
                    SectionId = 4,
                    BrandId = 2
                },
                new Product
                {
                    Id = 5,
                    Name = "Product-5",
                    Price = 1025,
                    ImageUrl = "product5.jpg",
                    Order = 4,
                    SectionId = 5,
                    BrandId = 2
                },
                new Product
                {
                    Id = 6,
                    Name = "Product-6",
                    Price = 1025,
                    ImageUrl = "product6.jpg",
                    Order = 5,
                    SectionId = 6,
                    BrandId = 3
                },
                new Product
                {
                    Id = 7,
                    Name = "Product-7",
                    Price = 1025,
                    ImageUrl = "product7.jpg",
                    Order = 6,
                    SectionId = 7,
                    BrandId = 3
                },
                new Product
                {
                    Id = 8,
                    Name = "Product-8",
                    Price = 1025,
                    ImageUrl = "product8.jpg",
                    Order = 7,
                    SectionId = 8,
                    BrandId = 4
                },
                new Product
                {
                    Id = 9,
                    Name = "Product-9",
                    Price = 1025,
                    ImageUrl = "product9.jpg",
                    Order = 8,
                    SectionId = 10,
                    BrandId = 5
                },
                new Product
                {
                    Id = 10,
                    Name = "Product-10",
                    Price = 1025,
                    ImageUrl = "product10.jpg",
                    Order = 9,
                    SectionId = 20,
                    BrandId = 6
                },
                new Product
                {
                    Id = 11,
                    Name = "Product-11",
                    Price = 1025,
                    ImageUrl = "product11.jpg",
                    Order = 10,
                    SectionId = 25,
                    BrandId = 6
                },
                new Product
                {
                    Id = 12,
                    Name = "Product-12",
                    Price = 1025,
                    ImageUrl = "product12.jpg",
                    Order = 11,
                    SectionId = 30,
                    BrandId = 7
                }
            };

        }

        public IEnumerable<Section> GetSections()
        {
            return Section;
        }

        public Section GetSectionById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Brand> GetBrands()
        {
            return Brand;
        }

        public Brand GetBrandById(int id)
        {
            throw new System.NotImplementedException();
        }

        public PagedProductDto GetProducts(ProductFilter filter)
        {
            var query = Products.AsQueryable();
            if (filter.BrandId.HasValue)
                query = query.Where(c => c.BrandId.HasValue &&
                                         c.BrandId.Value.Equals(filter.BrandId.Value));
            if (filter.SectionId.HasValue)
                query = query.Where(c =>
                    c.SectionId.Equals(filter.SectionId.Value));
            var model = new PagedProductDto
            {
                TotalCount = query.Count()
            };
            if (filter.PageSize.HasValue)
            {
                model.Products = query.OrderBy(c => c.Order).
                    Skip((filter.Page - 1) * filter.PageSize.Value).
                    Take(filter.PageSize.Value)
                    .Select(p =>
                        new ProductDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Order = p.Order,
                            Price = p.Price,
                            ImageUrl = p.ImageUrl,
                            Brand = p.BrandId.HasValue ? new BrandDto()
                            {
                                Id = p.Brand.Id,
                                Name = p.Brand.Name
                            } : null,
                            Section = new SectionDto()
                            {
                                Id = p.Section.Id,
                                Name = p.Section.Name
                            }
                        }).ToList();
            }
            else
            {
                model.Products = query.OrderBy(c => c.Order).Select(p =>
                    new ProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Order = p.Order,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl,
                        Brand = p.BrandId.HasValue ? new BrandDto()
                        {
                            Id =
                                p.Brand.Id,
                            Name = p.Brand.Name
                        } : null,
                        Section = new SectionDto()
                        {
                            Id = p.Section.Id,
                            Name = p.Section.Name
                        }
                    }).ToList();
            }
            return model;
        }

        public ProductDto GetProductById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}