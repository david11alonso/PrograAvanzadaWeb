using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrontEnd.API.Models;
using System.Net.Http;
using Newtonsoft.Json;
using data = FrontEnd.API.Models;
using Frontend.API.Controllers;
using static Frontend.API.Enums.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FrontEnd.API.Controllers
{
    [Authorize]
    public class ForosController : BaseController
    {
        string baseurl = "http://45.79.241.73/";
        private UserManager<IdentityUser> userManager;
        public ForosController(UserManager<IdentityUser> usrmg)
        {
            userManager = usrmg;
        }
        // GET: Foroes
        public async Task<IActionResult> Index()
        {
            List<data.Foro> aux = new List<data.Foro>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/Foro");

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.Foro>>(auxres);
                }
            }
            return View(aux);
        }
        public async Task<IActionResult> IndexEmpleado()
        {
            List<data.Foro> aux = new List<data.Foro>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/Foro");

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.Foro>>(auxres);
                }
            }
            return View(aux);
        }
        public async Task<IActionResult> IndexComentarios(int? id)
        {
            List<data.Comentario> aux = new List<data.Comentario>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/Comentario/"+id);

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.Comentario>>(auxres);
                }
            }
            ViewData["titulo"] = id;
            return View(aux);
        }
        // GET: Comentarios/Create
        public async Task<IActionResult> CreateComentario(int id)
        {
            List<Foro> listForo = new List<Foro>();
            listForo.Add(new Foro {ForoId = id });
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            List<IdentityUser> listUser = new List<IdentityUser>();
            listUser.Add(user);
            ViewData["ForoId"] = new SelectList(listForo, "ForoId", "ForoId");
            ViewData["UsuarioId"] = new SelectList(listUser, "Id", "Email");
            ViewData["titulo"] = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComentario([Bind("ComentarioId,Comentario1,ForoId,UsuarioId")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                using (var cl = new HttpClient())
                {
                    cl.BaseAddress = new Uri(baseurl);
                    var content = JsonConvert.SerializeObject(comentario);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var postTask = cl.PostAsync("api/Comentario", byteContent).Result;

                    if (postTask.IsSuccessStatusCode)
                    {
                        NotifyDelete("El comentario se ha agregado correctamente");
                        return RedirectToAction(nameof(IndexComentarios), new { id= comentario.ForoId});
                    }
                    else
                    {
                        NotifyError("Debe completar todos los campos para guardar un comentario.", notificationType: NotificationType.error);
                        return RedirectToAction(nameof(IndexComentarios), new { id = comentario.ForoId });
                    }
                }
            }
            NotifyError("El comentario no puede ser creado.", notificationType: NotificationType.error);
            return RedirectToAction(nameof(IndexComentarios), new { id = comentario.ForoId });
        }
        public async Task<IActionResult> DeleteComentario(int? id)
        {
            try
            {

                
                if (id == null)
                {
                    return NotFound();
                }
                else
                {
                    using (var cl = new HttpClient())
                    {
                        cl.BaseAddress = new Uri(baseurl);
                        cl.DefaultRequestHeaders.Clear();
                        cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage res = await cl.DeleteAsync("api/Comentario/" + id);

                        if (res.IsSuccessStatusCode)
                        {
                            NotifyDelete("El comentario se ha eliminado correctamente");
                            return RedirectToAction(nameof(IndexComentarios), new { id = @ViewData["titulo"] });
                        }
                    }
                }
            }
            catch (Exception)
            {
                NotifyError("El registro no puede ser eliminado. Contacte a su administrador", notificationType: NotificationType.error);
                return RedirectToAction(nameof(IndexComentarios), new { id= @ViewData["titulo"] });
            }

            return RedirectToAction(nameof(IndexComentarios), new { id = @ViewData["titulo"] });
        }



        // GET: Foroes/Create
        public IActionResult Create()
        {
            ViewData["PropuestaId"] = new SelectList(GetAllPropuestas(), "PropuestaId", "Titulo");
            return View();
        }

        // POST: Foroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ForoId,PropuestaId")] Foro foro)
        {
            if (ModelState.IsValid)
            {
                using(var cl = new HttpClient())
                {
                    cl.BaseAddress = new Uri(baseurl);
                    var content = JsonConvert.SerializeObject(foro);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var postTask = cl.PostAsync("api/Foro", byteContent).Result;

                    if (postTask.IsSuccessStatusCode)
                    {
                        NotifyDelete("El registro se ha agregado correctamente");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        NotifyError("El foro no puede ser creado porque ya existe uno asociado a la propuesta.", notificationType: NotificationType.error);
                        return RedirectToAction(nameof(Index));


                    }
                }
            }
            return View(foro);
        }


        // GET: Foroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var foro = GetById(id);
                if (foro == null)
                {
                    return NotFound();
                }
                else
                {
                    using (var cl = new HttpClient())
                    {
                        cl.BaseAddress = new Uri(baseurl);
                        cl.DefaultRequestHeaders.Clear();
                        cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage res = await cl.DeleteAsync("api/Foro/" + id);

                        if (res.IsSuccessStatusCode)
                        {
                            NotifyDelete("El registro se ha eliminado correctamente");
                            return RedirectToAction("Index");
                        }
                    }
                }

            }
            catch (Exception)
            {

                NotifyError("El registro no puede ser eliminado. Contacte a su administrador", notificationType: NotificationType.error);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Foroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.DeleteAsync("api/Foro/" + id);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ForoExists(int id)
        {
            return (GetById(id) != null);
        }

        private data.Foro GetById(int? id)
        {
            data.Foro aux = new data.Foro();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/Foro/" + id).Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<data.Foro>(auxres);
                }
            }
            return aux;
        }

        public List<data.Propuesta> GetAllPropuestas()
        {
            List<data.Propuesta> aux = new List<data.Propuesta>();

            using (var cl = new HttpClient())

            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/Propuesta").Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.Propuesta>>(auxres);
                }
            }

            return aux;
        }

    }
}
