using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.DO.Objects
{
    public class Propuesta
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

        public object Include(Func<object, object> p)
        {
            throw new NotImplementedException();
        }

        public virtual ICollection<PropuestaDepartamento> PropuestaDepartamento { get; set; }
        public virtual ICollection<VotoPropuesta> VotoPropuesta { get; set; }
    }
}
