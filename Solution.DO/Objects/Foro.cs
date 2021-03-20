using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.DO.Objects
{
    public class Foro
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
