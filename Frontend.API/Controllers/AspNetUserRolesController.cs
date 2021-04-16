using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrontEnd.API.Models;
using data = FrontEnd.API.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace FrontEnd.API.Controllers
{
    public class AspNetUserRolesController : Controller
    {
        string baseurl = "http://localhost:57096/";

        // GET: AspNetUserRoles
        public async Task<IActionResult> GetAllRoles()
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


        // POST: AspNetUserRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public bool Create(string userId, string roleId)
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

        public bool DeleteRole(string userId, string roleId)
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

        private bool AspNetUserRolesExists(string id)
        {
             return (GetRoleByUserId(id) != null);
        }

        public List<AspNetUserRoles> GetRoleByUserId(string id)
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

        public AspNetRoles GetRoleById(string id)
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

        public AspNetUsers GetUserById(string id)
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
    }
}
