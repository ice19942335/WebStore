using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Entities.Dto.Brand;
using WebStore.Entities.Dto.Page;
using WebStore.Entities.Dto.Product;
using WebStore.Entities.Entities;
using WebStore.Entities.ViewModels;
using WebStore.Interfaces.services;
using Xunit.Sdk;

namespace WebStore.Tests
{
    [TestClass]
    public class CatalogControllerTests
    {
        [TestMethod]
        public void ProductDetails_Returns_View_With_Correct_Item()
        {
            const int expected_product_id = 10;
            const string expected_product_name = "Product name";
            const int expected_order = 1;
            const decimal expected_price = 10;
            const string expected_image_url = "image.jpg";
            const int expected_brand_id = 1;
            const string expected_brand_name = "Brand name";

            var product_data_mock = new Mock<IProductData>();
            product_data_mock
                .Setup(p => p.GetProductById(It.IsAny<int>()))
                .Returns<int>(id => new ProductDto
                {
                    Id = id,
                    Name = expected_product_name,
                    Order = expected_order,
                    Price = expected_price,
                    ImageUrl = expected_image_url,
                    Brand = new BrandDto
                    {
                        Id = expected_brand_id,
                        Name = expected_brand_name
                    }
                });
            var configuration_mock = new Mock<IConfiguration>();

            var catalog_controller = new CatalogController(product_data_mock.Object, configuration_mock.Object);


            var result = catalog_controller.ProductDetails(expected_product_id);

            var view_result = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsAssignableFrom<ProductViewModel>(view_result.ViewData.Model);

            Xunit.Assert.Equal(expected_product_id, model.Id);
            Xunit.Assert.Equal(expected_product_name, model.Name);
            Xunit.Assert.Equal(expected_order, model.Order);
            Xunit.Assert.Equal(expected_price, model.Price);
            Xunit.Assert.Equal(expected_image_url, model.ImageUrl);
            Xunit.Assert.Equal(expected_brand_name, model.Brand.Name);
        }


        [TestMethod]
        public void ProductDetails_Returns_NotFound()
        {
            var products_data_mock = new Mock<IProductData>();
            products_data_mock
                .Setup(p => p.GetProductById(It.IsAny<int>()))
                .Returns((ProductDto)null);
            var configuration_mock = new Mock<IConfiguration>();

            var catalog_controller = new CatalogController(products_data_mock.Object, configuration_mock.Object);

            var result = catalog_controller.ProductDetails(1);
            Xunit.Assert.IsType<NotFoundResult>(result);
        }

        [TestMethod]
        public void Shop_Method_Returns_CorrectView()
        {
            const string expected_image1 = "image1.jpg";
            const string expected_image2 = "image2.jpg";
            const int expected_brand_id = 5;
            const int expected_section_id = 1;

            var products_data_mock = new Mock<IProductData>();
            products_data_mock
                .Setup(p => p.GetProducts(It.IsAny<ProductFilter>()))
                .Returns<ProductFilter>(filter => new PagedProductDto()
                {
                    Products = new[]
                    {
                        new ProductDto()
                        {
                            Id = 1,
                            Name = "Product 1",
                            ImageUrl = expected_image1,
                            Order = 1,
                            Price = 10,
                            Brand = new BrandDto()
                            {
                                Id = 1,
                                Name = "Brand"
                            }
                        },
                        new ProductDto()
                        {
                            Id = 2,
                            Name = "Product 2",
                            ImageUrl = expected_image2,
                            Order = 2,
                            Price = 11,
                            Brand = new BrandDto()
                            {
                                Id = 1,
                                Name = "Brand"
                            }
                        },
                    }
                });
            var configuration_mock = new Mock<IConfiguration>();
            configuration_mock
                .Setup(c => c["PageSize"])
                .Returns("3");

            var controller = new CatalogController(products_data_mock.Object, configuration_mock.Object);

            var result = controller.Shop(expected_section_id, expected_brand_id);

            var view_result = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsAssignableFrom<CatalogViewModel>(view_result.ViewData.Model);

            Xunit.Assert.Equal(2, model.Products.Count());
            Xunit.Assert.Equal(expected_brand_id, model.BrandId);
            Xunit.Assert.Equal(expected_section_id, model.SectionId);
            Xunit.Assert.Contains(expected_image1, model.Products.Select(p => p.ImageUrl));
            Xunit.Assert.Contains(expected_image2, model.Products.Select(p => p.ImageUrl));
        }
    }
}
