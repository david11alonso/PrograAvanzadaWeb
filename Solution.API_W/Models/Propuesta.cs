﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Solution.API_W.Models
{
    public partial class Propuesta
    {
        public Propuesta()
        {
            Foro = new HashSet<Foro>();
        }

        public int PropuestaId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public bool Pendiente { get; set; }
        public int Tipo { get; set; }
        public string UsuarioId { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public string Problema { get; set; }
        public string ResultadoEsperado { get; set; }
        public string Riesgos { get; set; }
        public string Beneficios { get; set; }

        public virtual AspNetUsers Usuario { get; set; }
        public virtual ICollection<Foro> Foro { get; set; }
    }
}
