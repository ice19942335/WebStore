using System.Collections.Generic;
using WebStore.Entities.Entities;

namespace WebStore.Data
{
    public static class TestData
    {
        public static List<Section> Sections { get; } = new List<Section>
        {
            new Section{Id = 1,Name = "Sportswear",Order = 0,ParentId = null},
            new Section{Id = 2,Name = "Nike",Order = 0,ParentId = 1},
            new Section{Id = 3,Name = "Under Armour",Order = 1,ParentId = 1},
            new Section{Id = 4,Name = "Adidas",Order = 2,ParentId = 1},
            new Section{Id = 5,Name = "Puma",Order = 3,ParentId = 1},
            new Section{Id = 6,Name = "ASICS",Order = 4,ParentId = 1},
            new Section{Id = 7,Name = "Mens",Order = 1,ParentId = null},
            new Section{Id = 8,Name = "Fendi",Order = 0,ParentId = 7},
            new Section{Id = 9,Name = "Guess",Order = 1,ParentId = 7},
            new Section{Id = 10,Name = "Valentino",Order = 2,ParentId = 7},
            new Section{Id = 11,Name = "Dior",Order = 3,ParentId = 7},
            new Section{Id = 12,Name = "Versace",Order = 4,ParentId = 7},
            new Section{Id = 13,Name = "Armani",Order = 5,ParentId = 7},
            new Section{Id = 14,Name = "Prada",Order = 6,ParentId = 7},
            new Section{Id = 15,Name = "Dolce and Gabbana",Order = 7,ParentId = 7},
            new Section{Id = 16,Name = "Chanel",Order = 8,ParentId = 7},
            new Section{Id = 17,Name = "Gucci",Order = 1,ParentId = 7},
            new Section{Id = 18,Name = "Womens",Order = 2,ParentId = null},
            new Section{Id = 19,Name = "Fendi",Order = 0,ParentId = 18},
            new Section{Id = 20,Name = "Guess",Order = 1,ParentId = 18},
            new Section{Id = 21,Name = "Valentino",Order = 2,ParentId = 18},
            new Section{Id = 22,Name = "Dior",Order = 3,ParentId = 18},
            new Section{Id = 23,Name = "Versace",Order = 4,ParentId = 18},
            new Section{Id = 24,Name = "Kids",Order = 3,ParentId = null},
            new Section{Id = 25,Name = "Fashion", Order = 4,ParentId = null},
            new Section{Id = 26,Name = "Households",Order = 5,ParentId = null},
            new Section{Id = 27,Name = "Interiors",Order = 6,ParentId = null},
            new Section{Id = 28,Name = "Clothing",Order = 7,ParentId = null},
            new Section{Id = 29,Name = "Bags",Order = 8,ParentId = null},
            new Section{Id = 30,Name = "Shoes",Order = 9,ParentId = null}
        };

        public static List<Brand> Brands { get; } = new List<Brand>
        {
            new Brand{Id = 1, Name = "Acne", Order = 0},
            new Brand{Id = 2, Name = "Grüne Erde", Order = 1},
            new Brand{Id = 3, Name = "Albiro", Order = 2},
            new Brand{Id = 4, Name = "Ronhill", Order = 3},
            new Brand{Id = 5, Name = "Oddmolly", Order = 4},
            new Brand{Id = 6, Name = "Boudestijn", Order = 5},
            new Brand{Id = 7, Name = "Rösch creative culture", Order = 6}
        };

        public static List<Product> Products { get; } = new List<Product>
        {
            new Product{Id = 1,Name = "Product-1",Price = 1025,ImageUrl = "product1.jpg",Order = 0,SectionId = 1,BrandId = 1},
            new Product{Id = 2,Name = "Product-2",Price = 1025,ImageUrl = "product2.jpg",Order = 1,SectionId = 2,BrandId = 1},
            new Product{Id = 3,Name = "Product-3",Price = 1025,ImageUrl = "product3.jpg",Order = 2,SectionId = 3,BrandId = 2},
            new Product{Id = 4,Name = "Product-4",Price = 1025,ImageUrl = "product4.jpg",Order = 3,SectionId = 4,BrandId = 2},
            new Product{Id = 5,Name = "Product-5",Price = 1025,ImageUrl = "product5.jpg",Order = 4,SectionId = 5,BrandId = 2},
            new Product{Id = 6,Name = "Product-6",Price = 1025,ImageUrl = "product6.jpg",Order = 5,SectionId = 6,BrandId = 3},
            new Product{Id = 7,Name = "Product-7",Price = 1025,ImageUrl = "product7.jpg",Order = 6,SectionId = 7,BrandId = 3},
            new Product{Id = 8,Name = "Product-8",Price = 1025,ImageUrl = "product8.jpg",Order = 7,SectionId = 8,BrandId = 4},
            new Product{Id = 9,Name = "Product-9",Price = 1025,ImageUrl = "product9.jpg",Order = 8,SectionId = 10,BrandId = 5},
            new Product{Id = 10,Name = "Product-10",Price = 1025,ImageUrl = "product10.jpg",Order = 9,SectionId = 20,BrandId = 6},
            new Product{Id = 11,Name = "Product-11",Price = 1025,ImageUrl = "product11.jpg",Order = 10,SectionId = 25,BrandId = 6},
            new Product{Id = 12,Name = "Product-12",Price = 1025,ImageUrl = "product12.jpg",Order = 11,SectionId = 30,BrandId = 7}
        };

    }
}