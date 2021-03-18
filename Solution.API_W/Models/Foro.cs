using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Solution.API_W.Models
{
    public partial class Foro
    {
        public Foro()
        {
            Comentario = new HashSet<Comentario>();
        }

        public int ForoId { get; set; }
        public int PropuestaId { get; set; }

        public virtual Propuesta Propuesta { get; set; }
        public virtual ICollection<Comentario> Comentario { get; set; }
    }
}
