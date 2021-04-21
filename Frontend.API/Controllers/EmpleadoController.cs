﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.API.Controllers
{
    public class EmpleadoController : BaseController
    {

        private UserManager<IdentityUser> userManager;
        private IPasswordHasher<IdentityUser> passwordHasher;

        public EmpleadoController(UserManager<IdentityUser> usrMgr, IPasswordHasher<IdentityUser> passwordHash)
        {
            userManager = usrMgr;
            passwordHasher = passwordHash;
        }

        public async Task<IActionResult> PasswordReset()
        {
            IdentityUser user = await userManager.GetUserAsync(User);
            ViewData["Id"] = user.Id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PasswordReset(IFormCollection values)
        {
            string newPassword = values["passwordNuevo"];

            IdentityUser user = await userManager.FindByIdAsync(values["Id"]);
            try
            {
                if (user != null)
                {
                    if (newPassword != null)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, newPassword);

                    }

                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                            throw new Exception(error.Description);
                    }

                }
                
            }
            catch (Exception e)
            {
                NotifyError(e.Message);
            }
            return View();
        }
    }
}
