using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FrontEnd.API.Models
{
    public partial class Departamento
    {
        public Departamento()
        {
            PropuestaDepartamento = new HashSet<PropuestaDepartamento>();
            UsuarioDepartamento = new HashSet<UsuarioDepartamento>();
        }

        public int DepartamentoId { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<PropuestaDepartamento> PropuestaDepartamento { get; set; }
        public virtual ICollection<UsuarioDepartamento> UsuarioDepartamento { get; set; }
    }
}
