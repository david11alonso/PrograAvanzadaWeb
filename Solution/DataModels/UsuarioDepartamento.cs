using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.API.DataModels
{
    public class UsuarioDepartamento
    {
        public int UsuarioDepartamentoId { get; set; }
        public string UsuarioId { get; set; }
        public int DepartamentoId { get; set; }

        public virtual Departamento Departamento { get; set; }
        public virtual AspNetUsers Usuario { get; set; }
    }
}
