using Frontend.API.Controllers;
using FrontEnd.API.Models;
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
using data = FrontEnd.API.Models;
using Microsoft.AspNetCore.Authorization;
using static Frontend.API.Enums.Enums;

namespace FrontEnd.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private UserManager<IdentityUser> userManager;
        private IPasswordHasher<IdentityUser> passwordHasher;
        string baseurl = "http://45.79.241.73/";

        public AdminController(UserManager<IdentityUser> usrMgr, IPasswordHasher<IdentityUser> passwordHash)
        {
            userManager = usrMgr;
            passwordHasher = passwordHash;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            ViewData["roles"] = getAllRoles();
            ViewData["departamentos"] = getAllDepartamentos();
            ViewData["usuarioDepartamentos"] = getAllUserDept();
            ViewData["UserRoles"] = getAllUserRoles();
            return View(userManager.Users);
        }

        public async Task<ViewResult> Create()
        {

            ViewData["roles"] = new SelectList(getAllRoles(), "Id", "Name");
            ViewData["departamentos"] = new SelectList(getAllDepartamentos(), "DepartamentoId", "Nombre");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(AspNetUsers user, IFormCollection formValues)
        {
            try
            {
            
            string role = formValues["rol"];
            string dept = formValues["departamento"];

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

                try
                {
                    if (result.Succeeded)
                    {
                        CreateUserRole(id, role);
                        CreateUserDepartment(id, Int32.Parse(dept));

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                            throw new Exception(error.Description);
                    }
                }
                catch (Exception e)
                {
                    NotifyError(e.Message);
                    ViewData["roles"] = new SelectList(getAllRoles(), "Id", "Name");
                    ViewData["departamentos"] = new SelectList(getAllDepartamentos(), "DepartamentoId", "Nombre");
                    return RedirectToAction("Create");
                }

            }

            ViewData["roles"] = new SelectList(getAllRoles(), "Id", "Name");
            ViewData["departamentos"] = new SelectList(getAllDepartamentos(), "DepartamentoId", "Nombre");
            return View(user);
            }
            catch (Exception)
            {
                NotifyError("Error.Faltan datos por completar.", notificationType: NotificationType.error);
                ViewData["roles"] = new SelectList(getAllRoles(), "Id", "Name");
                ViewData["departamentos"] = new SelectList(getAllDepartamentos(), "DepartamentoId", "Nombre");
                return RedirectToAction("Create");
            }
        }

        public async Task<IActionResult> Update(string id)
        {

            IdentityUser user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                var currentRole = GetRoleByUserId(user.Id);
                ViewData["roles"] = new SelectList(getAllRoles(), "Id", "Name", currentRole.FirstOrDefault().RoleId);
                var currentDept = getAllUserDept().Where(m => m.UsuarioId == id).FirstOrDefault().DepartamentoId;
                ViewData["departamentos"] = new SelectList(getAllDepartamentos(), "DepartamentoId", "Nombre", currentDept);

                return View(user);
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string password, IFormCollection formValues)
        {
            try
            {
           
            string role = formValues["rol"];
            var currentRole = GetRoleByUserId(id).FirstOrDefault().RoleId;
            var userDeptObj= getAllUserDept().Where(m => m.UsuarioId == id).FirstOrDefault();
            string dept = formValues["departamento"];

            IdentityUser user = await userManager.FindByIdAsync(id);

            try
            {
                if (user != null)
                {
                    if (!string.IsNullOrEmpty(email))
                        user.Email = email;
                    else
                        ModelState.AddModelError("", "El email no puede estar vacío");
                    if (!string.IsNullOrEmpty(email))
                    {
                        if (password != null)
                        {
                            user.PasswordHash = passwordHasher.HashPassword(user, password);

                        }

                        IdentityResult result = await userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            DeleteUserRole(id, currentRole);
                            CreateUserRole(id, role);
                            DeleteUserDepartment(userDeptObj.UsuarioDepartamentoId);
                            CreateUserDepartment(id, Int32.Parse(dept));
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            foreach (IdentityError error in result.Errors)
                                throw new Exception(error.Description);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                NotifyError(e.Message);
                ViewData["roles"] = new SelectList(getAllRoles(), "Id", "Name", currentRole);
                ViewData["departamentos"] = new SelectList(getAllDepartamentos(), "DepartamentoId", "Nombre", userDeptObj.DepartamentoId);

                return View(user);
            }

            ViewData["roles"] = new SelectList(getAllRoles(), "Id", "Name", currentRole);
            ViewData["departamentos"] = new SelectList(getAllDepartamentos(), "DepartamentoId", "Nombre", userDeptObj.DepartamentoId);

            return View(user);
            }
            catch (Exception)
            {
                NotifyError("Error.Faltan datos por completar.", notificationType: NotificationType.error);
                return RedirectToAction(nameof(Index));
            }
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }


        //[HttpPost]
        public async Task<IActionResult> Delete(string id)
        {

            IdentityUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    NotifyDelete("El registro se ha eliminado correctamente");
                    return RedirectToAction("Index");
                }
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "Usuario no encontrado");
            return View("Index", userManager.Users);
        }

        //// POST: Departamentoes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
 
        //            NotifyDelete("El registro se ha eliminado correctamente");
        //            return RedirectToAction("Index");

        //}

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

        private List<data.UsuarioDepartamento> getAllUserDept()
        {
            List<data.UsuarioDepartamento> aux = new List<data.UsuarioDepartamento>();

            using (var cl = new HttpClient())

            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/UsuarioDepartamento").Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.UsuarioDepartamento>>(auxres);
                }
            }

            return aux;
        }

        private List<data.AspNetUserRoles> getAllUserRoles()
        {
            List<data.AspNetUserRoles> aux = new List<data.AspNetUserRoles>();

            using (var cl = new HttpClient())

            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/AspNetUserRoles").Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.AspNetUserRoles>>(auxres);
                }
            }

            return aux;
        }

        private List<data.AspNetRoles> getAllRoles()
        {
            List<data.AspNetRoles> aux = new List<data.AspNetRoles>();

            using (var cl = new HttpClient())

            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/AspNetRoles").Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.AspNetRoles>>(auxres);
                }
            }

            return aux;
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

        private AspNetRoles GetRoleById(string id)
        {
            AspNetRoles aux = new AspNetRoles();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/AspNetRoles/" + id).Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<AspNetRoles>(auxres);
                }
            }
            return aux;
        }

        private AspNetUsers GetUserById(string id)
        {
            AspNetUsers aux = new AspNetUsers();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/AspNetUsers/" + id).Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<AspNetUsers>(auxres);
                }
            }
            return aux;
        }

        private Departamento GetDeptById(int id)
        {
            Departamento aux = new Departamento();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/Departamentos/" + id).Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<Departamento>(auxres);
                }
            }
            return aux;
        }

        private bool DeleteUserRole(string userId, string roleId)
        {

            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.DeleteAsync("api/AspNetUserRoles?userId=" + userId + "&roleId=" + roleId).Result;

                if (res.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        private async Task<IActionResult> GetAllRoles()
        {
            List<data.AspNetUserRoles> aux = new List<data.AspNetUserRoles>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/AspNetUserRoles");

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.AspNetUserRoles>>(auxres);
                }
            }
            return View(aux);
        }

        private bool CreateUserRole(string userId, string roleId)
        {
            var user = GetUserById(userId);
            var role = GetRoleById(roleId);
            var userRole = new AspNetUserRoles { Role = role, User = user };

            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                var content = JsonConvert.SerializeObject(userRole);
                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var postTask = cl.PostAsync("api/AspNetUserRoles", byteContent).Result;

                if (postTask.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }

        private bool CreateUserDepartment(string userId, int deptId)
        {
            var user = GetUserById(userId);
            var dept = GetDeptById(deptId);
            var userRole = new UsuarioDepartamento { Departamento = dept, Usuario = user };

            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                var content = JsonConvert.SerializeObject(userRole);
                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var postTask = cl.PostAsync("api/UsuarioDepartamento", byteContent).Result;

                if (postTask.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }

        private bool DeleteUserDepartment(int userDeptId)
        {

            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.DeleteAsync("api/UsuarioDepartamento/" + userDeptId).Result;

                if (res.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

    }
}
