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

namespace FrontEnd.API.Controllers
{
    public class VotoPropuestasController : Controller
    {
        /*
        string baseurl = "http://45.79.241.73/";

        // GET: VotoPropuestas
        public async Task<IActionResult> Index()
        {
            List<data.VotoPropuesta> aux = new List<data.VotoPropuesta>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/VotoPropuesta");

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.VotoPropuesta>>(auxres);
                }
            }
            return View(aux);
        }

        // GET: VotoPropuestas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votoPropuesta = GetById(id);
            if (votoPropuesta == null)
            {
                return NotFound();
            }

            return View(votoPropuesta);
        }

        // GET: VotoPropuestas/Create
        public IActionResult Create()
        {
            ViewData["PropuestaId"] = new SelectList(GetAllPropuestas(), "PropuestaId", "Beneficios");
            ViewData["UsuarioId"] = new SelectList(GetAllUsers(), "Id", "Id");
            return View();
        }

        // POST: VotoPropuestas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VotoPropuestaId,PropuestaId,Votacion,UsuarioId,VotoPropuesta")] VotoPropuesta votoPropuesta)
        {
            if (ModelState.IsValid)
            {
                using (var cl = new HttpClient())
                {
                    cl.BaseAddress = new Uri(baseurl);
                    var content = JsonConvert.SerializeObject(votoPropuesta);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var postTask = cl.PostAsync("api/VotoPropuesta", byteContent).Result;

                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            ViewData["PropuestaId"] = new SelectList(GetAllPropuestas(), "PropuestaId", "Beneficios", votoPropuesta.PropuestaId);
            ViewData["UsuarioId"] = new SelectList(GetAllUsers(), "Id", "Id", votoPropuesta.UsuarioId);
            return View(votoPropuesta);
        }

        // GET: VotoPropuestas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votoPropuesta = GetById(id);
            if (votoPropuesta == null)
            {
                return NotFound();
            }
            ViewData["PropuestaId"] = new SelectList(GetAllPropuestas(), "PropuestaId", "Beneficios", votoPropuesta.PropuestaId);
            ViewData["UsuarioId"] = new SelectList(GetAllUsers(), "Id", "Id", votoPropuesta.UsuarioId);
            return View(votoPropuesta);
        }

        // POST: VotoPropuestas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VotoPropuestaId,PropuestaId,Votacion,UsuarioId,VotoPropuesta")] VotoPropuesta votoPropuesta)
        {
            if (id != votoPropuesta.VotoPropuestaId)
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
                        var content = JsonConvert.SerializeObject(votoPropuesta);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        var postTask = cl.PutAsync("api/VotoPropuesta/" + id, byteContent).Result;

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
            ViewData["PropuestaId"] = new SelectList(GetAllPropuestas(), "PropuestaId", "Beneficios", votoPropuesta.PropuestaId);
            ViewData["UsuarioId"] = new SelectList(GetAllUsers(), "Id", "Id", votoPropuesta.UsuarioId);
            return View(votoPropuesta);
        }

        // GET: VotoPropuestas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votoPropuesta = GetById(id);
            if (votoPropuesta == null)
            {
                return NotFound();
            }

            return View(votoPropuesta);
        }

        // POST: VotoPropuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.DeleteAsync("api/VotoPropuesta/" + id);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool VotoPropuestaExists(int id)
        {
            return (GetById(id) != null);
        }

        private data.VotoPropuesta GetById(int? id)
        {
            data.VotoPropuesta aux = new data.VotoPropuesta();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/VotoPropuesta/" + id).Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<data.VotoPropuesta>(auxres);
                }
            }
            return aux;
        }

        private List<data.Propuesta> GetAllPropuestas()
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

        private List<data.AspNetUsers> GetAllUsers()
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

        */
    }
}
