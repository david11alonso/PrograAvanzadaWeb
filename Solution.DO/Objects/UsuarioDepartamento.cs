using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.DO.Objects
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

