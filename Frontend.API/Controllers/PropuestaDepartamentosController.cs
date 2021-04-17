﻿using System;
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
    public class PropuestaDepartamentosController : Controller
    {
        string baseurl = "http://45.79.241.73/";

        // GET: PropuestaDepartamentoes
        public async Task<IActionResult> Index()
        {
            List<data.Departamento> aux = new List<data.Departamento>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/Departamentos");

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.Departamento>>(auxres);
                }
            }
            return View(aux);
        }

        // Carga el index con las propuestas para un Departamento
        public ActionResult IndexPropuesta(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["DepartamentoId"] = id;

            var propuestaDepartamento = GetById(id);

            if (propuestaDepartamento == null)
            {
                return NotFound();
            }
            ViewData["DepartamentoId"] = id;
            ViewData["DepartamentoName"] = propuestaDepartamento.FirstOrDefault().Departamento.Nombre;


            return View(propuestaDepartamento);
        }

        // GET: PropuestaDepartamentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propuestaDepartamento = GetById(id);
            if (propuestaDepartamento == null)
            {
                return NotFound();
            }

            return View(propuestaDepartamento);
        }

        // GET: PropuestaDepartamentoes/Create
        public IActionResult Create(int? id)
        {
            var aux = getAllDepartamentos();
            List<Departamento> departamento = new List<Departamento>();
            foreach(Departamento dep in aux)
            {
                if(dep.DepartamentoId == id)
                {
                    departamento.Add(dep);
                }
            }
            ViewData["Departamento_Id"] = id;
           ViewData["DepartamentoId"] = new SelectList(departamento, "DepartamentoId", "Nombre");
            ViewData["PropuestaId"] = new SelectList(GetAllPropuestas(), "PropuestaId", "Titulo");
            return View();
        }

        // POST: PropuestaDepartamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropuestaDepartamentoId,PropuestaId,DepartamentoId")] PropuestaDepartamento propuestaDepartamento)
        {
            if (ModelState.IsValid)
            {
                using (var cl = new HttpClient())
                {
                    cl.BaseAddress = new Uri(baseurl);
                    var content = JsonConvert.SerializeObject(propuestaDepartamento);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var postTask = cl.PostAsync("api/PropuestaDepartamento", byteContent).Result;

                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(IndexPropuesta)+"/"+propuestaDepartamento.DepartamentoId);
                    }
                }
            }
            ViewData["DepartamentoId"] = new SelectList(getAllDepartamentos(), "DepartamentoId", "Nombre", propuestaDepartamento.DepartamentoId);
            ViewData["PropuestaId"] = new SelectList(GetAllPropuestas(), "PropuestaId", "Titulo", propuestaDepartamento.PropuestaId);
            return View(propuestaDepartamento);
        }

        // GET: PropuestaDepartamentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propuestaDepartamento = GetById(id).FirstOrDefault();
            if (propuestaDepartamento == null)
            {
                return NotFound();
            }
            ViewData["DepartamentoId"] = new SelectList(getAllDepartamentos(), "DepartamentoId", "Nombre", propuestaDepartamento.DepartamentoId);
            ViewData["PropuestaId"] = new SelectList(GetAllPropuestas(), "PropuestaId", "Titulo", propuestaDepartamento.PropuestaId);
            return View(propuestaDepartamento);
        }

        // POST: PropuestaDepartamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PropuestaDepartamentoId,PropuestaId,DepartamentoId")] PropuestaDepartamento propuestaDepartamento)
        {
            if (id != propuestaDepartamento.PropuestaDepartamentoId)
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
                        var content = JsonConvert.SerializeObject(propuestaDepartamento);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        var postTask = cl.PutAsync("api/PropuestaDepartamento/" + id, byteContent).Result;

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
            ViewData["DepartamentoId"] = new SelectList(getAllDepartamentos(), "DepartamentoId", "Nombre", propuestaDepartamento.DepartamentoId);
            ViewData["PropuestaId"] = new SelectList(GetAllPropuestas(), "PropuestaId", "Titulo", propuestaDepartamento.PropuestaId);
            return View(propuestaDepartamento);
        }

        // GET: PropuestaDepartamentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propuestaDepartamento = GetById(id);
            if (propuestaDepartamento == null)
            {
                return NotFound();
            }

            return View(propuestaDepartamento);
        }

        // POST: PropuestaDepartamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.DeleteAsync("api/PropuestaDepartamento/" + id);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PropuestaDepartamentoExists(int id)
        {
            return (GetById(id) != null);
        }

        private List<data.PropuestaDepartamento> GetById(int? id)
        {
            List <data.PropuestaDepartamento> aux = new List<data.PropuestaDepartamento>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/PropuestaDepartamento/" + id).Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject< List<data.PropuestaDepartamento>>(auxres);
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
