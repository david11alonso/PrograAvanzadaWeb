using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.DO.Objects
{
    public class Departamento
    {
        public Departamento()
        {
            PropuestaDepartamento = new HashSet<PropuestaDepartamento>();
            UsuarioDepartamento = new HashSet<UsuarioDepartamento>();
        }

        public int DepartamentoId { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<PropuestaDepartamento> PropuestaDepartamento { get; set; }
        public virtual ICollection<UsuarioDepartamento> UsuarioDepartamento { get; set; }
    }
}
