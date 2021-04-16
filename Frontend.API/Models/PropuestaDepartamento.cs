using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FrontEnd.API.Models
{
    public partial class PropuestaDepartamento
    {
        public int PropuestaDepartamentoId { get; set; }
        public int PropuestaId { get; set; }
        public int DepartamentoId { get; set; }

        public virtual Departamento Departamento { get; set; }
        public virtual Propuesta Propuesta { get; set; }
    }
}
