using FrontEnd.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.API.Models
{
    public class ReporteVotos
    {
        public virtual Propuesta Propuesta { get; set; }
        public int DeAcuerdo { get; set; }
        public int DeSacuerdo { get; set; }
        public int Neutral { get; set; }
    }
}
