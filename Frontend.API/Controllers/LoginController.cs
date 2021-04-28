using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FrontEnd.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;


namespace FrontEnd.API.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private UserManager<IdentityUser> userManager;
        string baseurl = "http://45.79.241.73/";
        public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> usrmgr)
        {
            this.signInManager = signInManager;
            this.userManager = usrmgr;
        }
        
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery(Name = "returnUrl")] string url)
        {
            if (url !=null)
            {
                ModelState.AddModelError(string.Empty, "Error. Por favor ingresar con un usuario autorizado.");
            }
            return View();
        }


        private List<AspNetUserRoles> GetRoleByUserId(string id)
        {
            List<AspNetUserRoles> aux = new List<AspNetUserRoles>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/AspNetUserRoles/" + id).Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<AspNetUserRoles>>(auxres);
                }
            }
            return aux;
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromQuery(Name = "returnUrl")] string url,LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                   if (url != null)
                    {
                        RedirectToAction(url);
                    }
                    else {
                        return RedirectToAction("index", "home");
                    }

                    
                    
                }

                ModelState.AddModelError(string.Empty, "Error de autenticacion. Por favor volver a intentarlo.");
            }

            return View(model);

        }
    }
}
