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


namespace FrontEnd.API.Controllers
{
    public class ForosController : BaseController
    {
        string baseurl = "http://45.79.241.73/";

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

        // GET: Foroes/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(foro);
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
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(foro);
        }

        // GET: Foroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["PropuestaId"] = new SelectList(GetAllPropuestas(), "PropuestaId", "Titulo", foro.PropuestaId);
            return View(foro);
        }

        // POST: Foroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ForoId,PropuestaId")] Foro foro)
        {
            if (id != foro.ForoId)
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
                        var content = JsonConvert.SerializeObject(foro);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        var postTask = cl.PutAsync("api/Foro/" + id, byteContent).Result;

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
            ViewData["PropuestaId"] = new SelectList(GetAllPropuestas(), "PropuestaId", "Titulo", foro.PropuestaId);
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
