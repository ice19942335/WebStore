﻿using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models.Cart;
using WebStore.Models.Order;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrdersService _ordersService;

        public CartController(ICartService cartService, IOrdersService ordersService)
        {
            _cartService = cartService;
            _ordersService = ordersService;
        }

        [Breadcrumb("Cart")]
        public IActionResult Details()
        {
            var model = new DetailsViewModel()
            {
                CartViewModel = _cartService.TransformCart(),
                OrderViewModel = new OrderViewModel()
            };
            return View(model);

        }

        public IActionResult DecrementFromCart(int id)
        {
            _cartService.DecrementFromCart(id);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveAll()
        {
            _cartService.RemoveAll();
            return RedirectToAction("Details");
        }

        public IActionResult AddToCart(int id, string returnUrl)
        {
            _cartService.AddToCart(id);
            return Redirect(returnUrl);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CheckOut(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var orderResult = _ordersService.CreateOrder(model, _cartService.TransformCart(), User.Identity.Name);
                _cartService.RemoveAll();
                if (orderResult.Id.Equals(0)) //Will be true IF CreateOrder method will return a (New Order)
                    return View("YouHaveToBeRegistredUser");
                return RedirectToAction("OrderConfirmed", new { id = orderResult.Id });
            }

            var detailsModel = new DetailsViewModel
            {
                CartViewModel = _cartService.TransformCart(),
                OrderViewModel = model
            };
            return View("Details", detailsModel);
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}