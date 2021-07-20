using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Practice9july.Models;
using Practice9july.Models.ViewModels;

namespace Practice9july.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User {FirstName = viewModel.FirstName, LastName = viewModel.LastName, Email = viewModel.Email, UserName = viewModel.FirstName + viewModel.LastName};

                var result = await _userManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var identityError in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, identityError.Description);
                    }
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    ControllerContext.HttpContext.Response.Cookies.Append("UserId", user.Id);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Неправильные данные для входа или пароль");
                }
            }

            return View(model);
        }
        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Todos()
        {
            return View();
        }
    }
}
