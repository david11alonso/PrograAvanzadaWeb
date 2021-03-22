using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.DO.Objects
{
    public class PropuestaDepartamento
    {
        public int PropuestaDepartamentoId { get; set; }
        public int PropuestaId { get; set; }
        public int DepartamentoId { get; set; }

        public virtual Departamento Departamento { get; set; }
        public virtual Propuesta Propuesta { get; set; }
    }
}
