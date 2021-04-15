using System;
using System.Threading.Tasks;
using FrontEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private UserManager<IdentityUser> userManager;

        public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> usrmgr)
        {
            this.signInManager = signInManager;
            this.userManager = usrmgr;
        }
        /*
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }*/

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction("index", "home");

                    }else if (User.IsInRole("Empleado"))
                    {
                        return RedirectToAction("index", "homeEmpleado");

                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);

        }
    }
}
