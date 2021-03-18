using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Solution.API_W.Models
{
    public partial class Noticia
    {
        public int NoticiaId { get; set; }
        public string UsuarioId { get; set; }
        public string Descripcion { get; set; }

        public virtual AspNetUsers Usuario { get; set; }
    }
}
