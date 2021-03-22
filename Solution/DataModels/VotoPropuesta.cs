using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.API.DataModels
{
    public class VotoPropuesta
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
