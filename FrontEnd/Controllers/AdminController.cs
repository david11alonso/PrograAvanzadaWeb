using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class AdminController : Controller
    {
        private readonly PrograAvanzadaWebContext _context = new PrograAvanzadaWebContext();
        private UserManager<IdentityUser> userManager;
        private IPasswordHasher<IdentityUser> passwordHasher;

        public AdminController(UserManager<IdentityUser> usrMgr, IPasswordHasher<IdentityUser> passwordHash)
        {
            userManager = usrMgr;
            passwordHasher = passwordHash;
        }

        // GET: Admin
        public IActionResult Index()
        {
            ViewData["roles"] = new SelectList(_context.AspNetRoles, "Id", "Name");
            return View(userManager.Users);
        }

        public ViewResult Create()
        {

            ViewData["roles"] = new SelectList(_context.AspNetRoles, "Id", "Name");

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
                    UserName = user.Email,
                    NormalizedUserName = user.Email,
                    NormalizedEmail = user.Email,
                    Email = user.Email,
                    EmailConfirmed = true

                };
                string id = appUser.Id;
                IdentityResult result = await userManager.CreateAsync(appUser, user.PasswordHash);

                if (result.Succeeded)
                {
                    new AspNetUserRolesController(_context).CreateUserRole(id, role);

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
        public async Task<IActionResult> Update(string id, string email, string password, string normalizeduserName, IFormCollection formValues)
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
                if (!string.IsNullOrEmpty(email))
                    user.NormalizedUserName = normalizeduserName;
                else
                    ModelState.AddModelError("", "El nombre no puede estar vacío");
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
    }
}