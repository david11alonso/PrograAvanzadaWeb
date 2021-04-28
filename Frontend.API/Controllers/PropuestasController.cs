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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FrontEnd.API.Controllers
{
    [Authorize]
    public class PropuestasController : BaseController
    {
        private UserManager<IdentityUser> userManager;

        string baseurl = "http://45.79.241.73/";
        public PropuestasController(UserManager<IdentityUser> usrmg)
        {
            userManager = usrmg;
        }

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
            if (User.IsInRole("Empleado"))
                return RedirectToAction("PropuestasEmpleado");
            return View(aux);
        }
        // GET: Propuestas
        public async Task<IActionResult> IndexAprobacion()
        {
            List<data.Propuesta> aux = new List<data.Propuesta>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/Propuesta/Pendiente");

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.Propuesta>>(auxres);
                }
            }
            return View(aux);
        }
        public async Task<IActionResult> Aprobar(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                using (var cl = new HttpClient())
                {
                    cl.BaseAddress = new Uri(baseurl);
                    cl.DefaultRequestHeaders.Clear();
                    cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage res = await cl.GetAsync("api/Propuesta/Aprobacion/" + id);

                    if (res.IsSuccessStatusCode)
                    {
                        NotifyDelete("El registro se ha aprobado correctamente");
                        return RedirectToAction("IndexAprobacion");
                    }
                }


            }
            catch (Exception)
            {

                NotifyError("El registro no puede ser aprobado. Contacte a su administrador", notificationType: NotificationType.error);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> IndexEmpleado()
        {
            List<data.PropuestaDepartamento> auxPD = new List<data.PropuestaDepartamento>();
            List<data.PropuestaDepartamento> auxD = new List<data.PropuestaDepartamento>();
            List<data.VotoPropuesta> auxVP = new List<data.VotoPropuesta>();
            List<data.Propuesta> propuestas = new List<data.Propuesta>();
            var user =await userManager.FindByNameAsync(User.Identity.Name);
            Boolean b = true;
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/PropuestaDepartamento");
                HttpResponseMessage res2 = await cl.GetAsync("api/UsuarioDepartamento/"+user.Id);
                HttpResponseMessage res3 = await cl.GetAsync("api/VotoPropuesta");
                if (res.IsSuccessStatusCode && res2.IsSuccessStatusCode)
                {
                    var auxresPD = res.Content.ReadAsStringAsync().Result;
                    auxPD = JsonConvert.DeserializeObject<List<data.PropuestaDepartamento>>(auxresPD);
                    var auxresD = res2.Content.ReadAsStringAsync().Result;
                    auxD = JsonConvert.DeserializeObject<List<data.PropuestaDepartamento>>(auxresD);
                    var auxresVP = res3.Content.ReadAsStringAsync().Result;
                    auxVP = JsonConvert.DeserializeObject<List<data.VotoPropuesta>>(auxresVP);
                    foreach (PropuestaDepartamento pd in auxPD)
                    {
                        if(pd.DepartamentoId == auxD.FirstOrDefault().DepartamentoId && pd.Propuesta.Pendiente == false)
                        {
                            b = true;
                            foreach (VotoPropuesta vp in auxVP)
                            {
                                if(vp.UsuarioId == user.Id && vp.PropuestaId == pd.PropuestaId)
                                {
                                    b = false;
                                }
                            }
                            if (b) { propuestas.Add(pd.Propuesta); }
                            
                        }
                    }
                }
            }
            return View(propuestas);
        }

        // GET: VotoPropuestas/Create
        public async Task<IActionResult> VotarAsync(int id)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            List<IdentityUser> listUser = new List<IdentityUser>();
            listUser.Add(user);
            List<Propuesta> listPropuesta = new List<Propuesta>();
            listPropuesta.Add(GetById(id));
            ViewData["PropuestaId"] = new SelectList(listPropuesta, "PropuestaId", "Titulo");
            ViewData["UsuarioId"] = new SelectList(listUser, "Id", "Email");
            return View();
        }

        // POST: VotoPropuestas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Votar([Bind("VotoPropuestaId,PropuestaId,Votacion,UsuarioId,VotoPropuesta,Comentario")] VotoPropuesta votoPropuesta)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (String.IsNullOrEmpty(votoPropuesta.Comentario))
                    {
                        NotifyError("El voto no pudo ser registrado ya que falto informacion por llenar. Intente de nuevo.", notificationType: NotificationType.error);
                        return RedirectToAction(nameof(IndexEmpleado));
                    }
                    else
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
                                NotifyDelete("El registro se ha agregado correctamente");
                                return RedirectToAction(nameof(IndexEmpleado));
                            }
                            else
                            {
                                NotifyError("Error inesperado. Contacte a su administrador", notificationType: NotificationType.error);
                                return RedirectToAction(nameof(IndexEmpleado));
                            }
                        }
                    }

                }
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                List<IdentityUser> listUser = new List<IdentityUser>();
                listUser.Add(user);
                List<Propuesta> listPropuesta = new List<Propuesta>();
                listPropuesta.Add(GetById(votoPropuesta.PropuestaId));
                ViewData["PropuestaId"] = new SelectList(listPropuesta, "PropuestaId", "Beneficios", votoPropuesta.PropuestaId);
                ViewData["UsuarioId"] = new SelectList(listUser, "Id", "Email", votoPropuesta.UsuarioId);
                return View(votoPropuesta);
            }
            catch (Exception)
            {
                NotifyError("Error inesperado. Contacte a su administrador", notificationType: NotificationType.error);
                return RedirectToAction(nameof(IndexEmpleado));
            }


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
        public async Task<IActionResult> CreateAsync()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            List<IdentityUser> listUser = new List<IdentityUser>();
            listUser.Add(user);
            ViewData["UsuarioId"] = new SelectList(listUser, "Id", "Email");
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
                        if (User.IsInRole("Empleado"))
                            return RedirectToAction("PropuestasEmpleado");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        NotifyError("El registro no puede ser creado ya que no se completaron todos los datos.", notificationType: NotificationType.error);
                        return RedirectToAction(nameof(Create));
                    }
                }
            }
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            List<IdentityUser> listUser = new List<IdentityUser>();
            listUser.Add(user);
            ViewData["UsuarioId"] = new SelectList(listUser, "Id", "Email", propuesta.UsuarioId);
            return View(propuesta);
        }


        // GET: Propuestas/Create
        public async Task<IActionResult> ProponerAsync()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            List<IdentityUser> listUser = new List<IdentityUser>();
            listUser.Add(user);
            ViewData["UsuarioId"] = new SelectList(listUser, "Id", "Email");
            return View();
        }

        // POST: Propuestas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Proponer([Bind("PropuestaId,Titulo,Descripcion,Tipo,UsuarioId,FechaPublicacion,FechaFinalizacion,Problema,ResultadoEsperado,Riesgos,Beneficios")] Propuesta propuesta)
        {
            if (ModelState.IsValid)
            {
                using (var cl = new HttpClient())
                {
                    propuesta.Pendiente = true;
                    cl.BaseAddress = new Uri(baseurl);
                    var content = JsonConvert.SerializeObject(propuesta);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var postTask = cl.PostAsync("api/Propuesta", byteContent).Result;

                    if (postTask.IsSuccessStatusCode)
                    {
                        NotifyDelete("El registro se ha agregado correctamente");
                        return RedirectToAction(nameof(Proponer));
                    }
                    else
                    {
                        NotifyError("El registro no puede ser creado ya que no se completaron todos los datos.", notificationType: NotificationType.error);
                        return RedirectToAction(nameof(Proponer));
                    }
                }
            }
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            List<IdentityUser> listUser = new List<IdentityUser>();
            listUser.Add(user);
            ViewData["UsuarioId"] = new SelectList(listUser, "Id", "Email", propuesta.UsuarioId);
            return RedirectToAction(nameof(Proponer));
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
                    if (User.IsInRole("Empleado"))
                        propuesta.Pendiente = true;
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
                            if (User.IsInRole("Empleado"))
                                return RedirectToAction("PropuestasEmpleado");
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

        public async Task<IActionResult> DeleteAprobacion(int? id)
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
                            NotifyDelete("El registro se ha rechazado correctamente");

                            return RedirectToAction("IndexAprobacion");
                        }
                    }
                }
            }
            catch (Exception)
            {

                NotifyError("El registro no puede ser rechazado. Contacte a su administrador", notificationType: NotificationType.error);
            }

            return RedirectToAction(nameof(IndexAprobacion));
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
                    if (User.IsInRole("Empleado"))
                        return RedirectToAction("PropuestasEmpleado");
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

        public async Task<IActionResult> PropuestasEmpleado()
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

            IdentityUser user = await userManager.GetUserAsync(User);
            return View("PropuestasEmpleado", aux.Where(p => p.UsuarioId == user.Id) );
        }
    }
}

