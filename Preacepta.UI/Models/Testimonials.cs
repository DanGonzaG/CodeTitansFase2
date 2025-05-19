using System.ComponentModel;

namespace Praecepta.UI.Models
{
    public class Testimonials
    {
        [DisplayName("Fecha")]
        public DateTime fecha { get; set; }
        public bool Reportar { get; set; }
        [DisplayName("Nombre Cliente")]
        public string NomCliente { get; set; }
        public int Identificacion { get; set; }
        public string Telefono { get; set; }
        public string Comentario {  get; set; }
        public int Calificacion { get; set; }

    }
}
