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
using Frontend.API.Controllers;
using static Frontend.API.Enums.Enums;

namespace FrontEnd.API.Controllers
{
    public class PropuestasController : BaseController
    {

        string baseurl = "http://45.79.241.73/";

        // GET: Propuestas
        public async Task<IActionResult> Index()
        {
            List<data.Propuesta> aux = new List<data.Propuesta>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/Propuesta");

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.Propuesta>>(auxres);
                }
            }
            return View(aux);
        }

        public async Task<IActionResult> IndexEmpleado()
        {
            List<data.Propuesta> aux = new List<data.Propuesta>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/Propuesta");

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.Propuesta>>(auxres);
                }
            }
            return View(aux);
        }


        // GET: Propuestas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propuesta = GetById(id);

            if (propuesta == null)
            {
                return NotFound();
            }

            return View(propuesta);
        }

        // GET: Propuestas/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(GetAllUsers(), "Id", "Email");
            return View();
        }

        // POST: Propuestas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropuestaId,Titulo,Descripcion,Pendiente,Tipo,UsuarioId,FechaPublicacion,FechaFinalizacion,Problema,ResultadoEsperado,Riesgos,Beneficios")] Propuesta propuesta)
        {
            if (ModelState.IsValid)
            {
                using (var cl = new HttpClient())
                {
                    cl.BaseAddress = new Uri(baseurl);
                    var content = JsonConvert.SerializeObject(propuesta);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var postTask = cl.PostAsync("api/Propuesta", byteContent).Result;

                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            ViewData["UsuarioId"] = new SelectList(GetAllUsers(), "Id", "Email", propuesta.UsuarioId);
            return View(propuesta);
        }

        // GET: Propuestas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propuesta = GetById(id);

            if (propuesta == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(GetAllUsers(), "Id", "Email", propuesta.UsuarioId);
            return View(propuesta);
        }

        // POST: Propuestas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PropuestaId,Titulo,Descripcion,Pendiente,Tipo,UsuarioId,FechaPublicacion,FechaFinalizacion,Problema,ResultadoEsperado,Riesgos,Beneficios")] Propuesta propuesta)
        {
            if (id != propuesta.PropuestaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var cl = new HttpClient())
                    {
                        cl.BaseAddress = new Uri(baseurl);
                        var content = JsonConvert.SerializeObject(propuesta);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        var postTask = cl.PutAsync("api/Propuesta/" + id, byteContent).Result;

                        if (postTask.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
                catch (Exception)
                {
                    var aux2 = GetById(id);
                    if (aux2 == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(GetAllUsers(), "Id", "Email", propuesta.UsuarioId);
            return View(propuesta);
        }

        // GET: Propuestas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {

                if (id == null)
                {
                    return NotFound();
                }

                var propuesta = GetById(id);

                if (propuesta == null)
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
                        HttpResponseMessage res = await cl.DeleteAsync("api/Propuesta/" + id);

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

        // POST: Propuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.DeleteAsync("api/Propuesta/" + id);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PropuestaExists(int id)
        {
            return (GetById(id) != null);
        }

        private data.Propuesta GetById(int? id)
        {
            data.Propuesta aux = new data.Propuesta();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/Propuesta/" + id).Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<data.Propuesta>(auxres);
                }
            }
            return aux;
        }

        public List<data.AspNetUsers> GetAllUsers()
        {
            List<data.AspNetUsers> aux = new List<data.AspNetUsers>();

            using (var cl = new HttpClient())

            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/AspNetUsers").Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.AspNetUsers>>(auxres);
                }
            }

            return aux;
        }
    }
}

