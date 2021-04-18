namespace FrontEnd.API.Models
{
    public class ReporteVotos
    {
        public virtual Propuesta Propuesta { get; set; }
        public int DeAcuerdo { get; set; }
        public int DeSacuerdo { get; set; }
        public int Neutral { get; set; }
    }
}