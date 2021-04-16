using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEnd.API.Controllers
{
    public class AdminController : Controller
    {
        private readonly PrograAvanzadaWebContext _context = new PrograAvanzadaWebContext();
        private UserManager<IdentityUser> userManager;
        private IPasswordHasher<IdentityUser> passwordHasher;
        string baseurl = "http://localhost:57096/";

        public AdminController(UserManager<IdentityUser> usrMgr, IPasswordHasher<IdentityUser> passwordHash)
        {
            userManager = usrMgr;
            passwordHasher = passwordHash;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            ViewData["roles"] = new SelectList(_context.AspNetRoles, "Id", "Name");
            ViewData["departamentos"] = new SelectList(getAllDepartamentos(), "DepartamentoId", "Nombre");
            return View(userManager.Users);
        }

        public async Task<ViewResult> Create()
        {

            ViewData["roles"] = new SelectList(_context.AspNetRoles, "Id", "Name");
            var controller = new DepartamentosController();
            List<Departamento> departamentos = await controller.GetAll();
            ViewData["departamentos"] = new SelectList(departamentos, "DepartamentoId", "Nombre");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(AspNetUsers user, IFormCollection formValues)
        {
            string role = formValues["rol"];


            if (ModelState.IsValid)
            {
                IdentityUser appUser = new IdentityUser
                {
                    UserName = user.NormalizedUserName.Replace(" ", ""),
                    NormalizedUserName = user.NormalizedUserName,
                    Email = user.Email,
                    EmailConfirmed = true

                };
                string id = appUser.Id;
                IdentityResult result = await userManager.CreateAsync(appUser, user.PasswordHash);

                if (result.Succeeded)
                {
                    new AspNetUserRolesController(_context).CreateUserRole(id, role);
                    var controller = new DepartamentosController();


                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Update(string id)
        {

            IdentityUser user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                ViewData["roles"] = new SelectList(_context.AspNetRoles, "Id", "Name", new AspNetUserRolesController(_context).getRole(user.Id));
                //ViewData["roleAsignado"] = new AspNetUserRolesController(_context).getRole(user.Id);

                return View(user);
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string password, IFormCollection formValues)
        {
            string role = formValues["rol"];

            IdentityUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                else
                    ModelState.AddModelError("", "El email no puede estar vacío");

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "La contraseña no puede estar vacía");

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var aux = new AspNetUserRolesController(_context);
                        aux.DeleteRole(id);
                        aux.CreateUserRole(id, role);
                        return RedirectToAction("Index");
                    }
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "Usuario no encontrado");
            return View(user);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "Usuario no encontrado");
            return View("Index", userManager.Users);
        }

        private List<data.Departamento> getAllDepartamentos()
        {
            List<data.Departamento> aux = new List<data.Departamento>();

            using (var cl = new HttpClient())

            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/Departamentos").Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.Departamento>>(auxres);
                }
            }

            return aux;
        }
    }
}
