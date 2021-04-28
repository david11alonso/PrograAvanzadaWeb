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
using Microsoft.AspNetCore.Authorization;

namespace Frontend.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReporteVotosController : Controller
    {
        string baseurl = "http://45.79.241.73/";
        public async Task<IActionResult> IndexAsync()
        {
            List<data.Propuesta> propuestas = new List<data.Propuesta>();
            List<data.VotoPropuesta> votoPropuestas = new List<data.VotoPropuesta>();
            List<data.ReporteVotos> reporteVotos = new List<data.ReporteVotos>();
            int i = 0;
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/Propuesta");
                HttpResponseMessage res2 = await cl.GetAsync("api/VotoPropuesta");

                if (res.IsSuccessStatusCode && res2.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    var auxres2 = res2.Content.ReadAsStringAsync().Result;
                    propuestas = JsonConvert.DeserializeObject<List<data.Propuesta>>(auxres);
                    votoPropuestas = JsonConvert.DeserializeObject<List<data.VotoPropuesta>>(auxres2);
                    foreach(data.Propuesta propuestaAux in propuestas)
                    {
                        reporteVotos.Add(new ReporteVotos { Propuesta = propuestaAux,DeAcuerdo = 0, DeSacuerdo = 0, Neutral=0});
                        foreach (data.VotoPropuesta votoPropuestaAux in votoPropuestas)
                        {
                            if(votoPropuestaAux.PropuestaId == propuestaAux.PropuestaId)
                            {
                                if (votoPropuestaAux.Votacion == 0)
                                {
                                    reporteVotos[i].DeSacuerdo++;
                                }
                                else if(votoPropuestaAux.Votacion == 1)
                                {
                                    reporteVotos[i].Neutral++;
                                }
                                else if (votoPropuestaAux.Votacion == 2)
                                {
                                    reporteVotos[i].DeAcuerdo++;
                                }
                               
                            }
                        }
                        i++;
                    }
                }

            }
            return View(reporteVotos);
        }

        public async Task<IActionResult> IndexComentarios(int? id, string titulo)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reporteVoto = GetById(id);
            if (reporteVoto == null)
            {
                return NotFound();
            }
            return View(reporteVoto);
        }

        private List<data.VotoPropuesta> GetById(int? id)
        {
            List < data.VotoPropuesta> aux = new List<data.VotoPropuesta>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/VotoPropuesta/" + id).Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject< List<data.VotoPropuesta>>(auxres);
                }
            }
            return aux;
        }

    }
}
