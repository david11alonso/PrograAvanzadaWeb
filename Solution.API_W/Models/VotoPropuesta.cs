using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Solution.API_W.Models
{
    public partial class VotoPropuesta
    {
        public int VotoPropuestaId { get; set; }
        public int PropuestaId { get; set; }
        public int Votacion { get; set; }
        public string UsuarioId { get; set; }
        public string Comentario { get; set; }

        public virtual Propuesta Propuesta { get; set; }
        public virtual AspNetUsers Usuario { get; set; }
    }
}
