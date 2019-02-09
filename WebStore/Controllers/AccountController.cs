using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs;
using WebStore.DomainNew.Entities;
using WebStore.Models.Account;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> SignInManager)
        {
            _userManager = userManager;
            _signInManager = SignInManager;
        }

        [HttpGet] public IActionResult Login() => View(new LoginUserViewModel());

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var loginResult = await _signInManager.PasswordSignInAsync(
                model.UserName, model.Password, model.RememberMe, false);
            if (loginResult.Succeeded)
                return Url.IsLocalUrl(model.ReturnUrl)
                    ? RedirectToAction(model.ReturnUrl)
                    : RedirectToAction("Index", "Home");
            ModelState.AddModelError("", "Неверное имя пользователя либо пароль");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = new User { UserName = model.UserName };
            var creationResult = await _userManager.CreateAsync(user, model.Password);
            if (creationResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                await _userManager.AddToRoleAsync(user, "User");
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            foreach (var identityError in creationResult.Errors)
                ModelState.AddModelError("", identityError.Description);

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}