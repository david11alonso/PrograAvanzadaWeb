using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FrontEnd.Models
{
    public partial class Comentario
    {
        public int ForoId { get; set; }
        public string UsuarioId { get; set; }
        public string Comentario1 { get; set; }
        public int ComentarioId { get; set; }

        public virtual Foro Foro { get; set; }
        public virtual AspNetUsers Usuario { get; set; }
    }
}
