using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.API.DataModels
{
    public class Comentario
    {
        public int ForoId { get; set; }
        public string UsuarioId { get; set; }
        public string Comentario1 { get; set; }
        public int ComentarioId { get; set; }

        public virtual Foro Foro { get; set; }
        public virtual AspNetUsers Usuario { get; set; }
    }
}
