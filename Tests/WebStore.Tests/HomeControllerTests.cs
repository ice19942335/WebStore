using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Interfaces.services;

namespace WebStore.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _controller;

        [TestInitialize]
        public void SetupTest()
        {
            var mockService = new Mock<IValuesService>();
            mockService.Setup(c => c.GetAsync()).ReturnsAsync(new List<string> {
                "1", "2" });
            _controller = new HomeController(mockService.Object);
        }

        [TestMethod]
        public async Task Index_Method_Returns_View_With_Values()
        {
            // Arrange and act
            var result = await _controller.Index();
            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsAssignableFrom<IEnumerable<string>>(
                viewResult.ViewData.Model);
            Xunit.Assert.Equal(2, model.Count());
        }

        [TestMethod]
        public void Checkout_Returns_View()
        {
            var result = _controller.Checkout();
            Xunit.Assert.IsType<ViewResult>(result);
        }
        [TestMethod]
        public void BlogSingle_Returns_View()
        {
            var result = _controller.BlogSingle();
            Xunit.Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Blog_Returns_View()
        {
            var result = _controller.Blog();
            Xunit.Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void NotFound_Returns_View()
        {
            var result = _controller.NotFound404();
            Xunit.Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ErrorStatus_404_Redirects_to_NotFound()
        {
            var result = _controller.ErrorStatus("404");
            var redirectToActionResult =
                Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Null(redirectToActionResult.ControllerName);
            Xunit.Assert.Equal("NotFound404", redirectToActionResult.ActionName);
        }

        [TestMethod]
        public void ErrorStatus_Antother_Returns_Content_Result()
        {
            var result = _controller.ErrorStatus("500");
            var contentResult = Xunit.Assert.IsType<ContentResult>(result);
            Xunit.Assert.Equal("Error status code: 500", contentResult.Content);
        }

    }
}
