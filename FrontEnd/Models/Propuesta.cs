using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FrontEnd.Models
{
    public partial class Propuesta
    {
        public Propuesta()
        {
            Foro = new HashSet<Foro>();
            PropuestaDepartamento = new HashSet<PropuestaDepartamento>();
            VotoPropuesta = new HashSet<VotoPropuesta>();
        }

        public int PropuestaId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public bool Pendiente { get; set; }
        public string UsuarioId { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public string Problema { get; set; }
        public string ResultadoEsperado { get; set; }
        public string Riesgos { get; set; }
        public string Beneficios { get; set; }

        public virtual AspNetUsers Usuario { get; set; }
        public virtual ICollection<Foro> Foro { get; set; }
        public virtual ICollection<PropuestaDepartamento> PropuestaDepartamento { get; set; }
        public virtual ICollection<VotoPropuesta> VotoPropuesta { get; set; }
    }
}
