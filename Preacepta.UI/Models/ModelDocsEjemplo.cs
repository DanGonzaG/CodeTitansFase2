using System.ComponentModel;

namespace Praecepta.UI.Models
{
    public class ModelDocsEjemplo
    {
        public DateOnly Fecha { get; set; }

        [DisplayName("Tipo de Documento")]
        public string TipoDocumento { get; set; }
        public string Cliente { get; set; }
        public string Abogado { get; set; }
    }
}
