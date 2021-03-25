using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.API.DataModels
{
    public class Noticia
    {
        public int NoticiaId { get; set; }
        public string UsuarioId { get; set; }
        public string Descripcion { get; set; }

        public virtual AspNetUsers Usuario { get; set; }
    }
}
