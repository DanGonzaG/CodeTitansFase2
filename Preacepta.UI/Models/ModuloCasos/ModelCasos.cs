using System.ComponentModel;
using System.Security.Principal;

namespace Praecepta.UI.Models.ModuloCasos
{
    public class ModelCasos
    {
        [DisplayName("Numero")]
        public int NunCaso { get; set; }

        [DisplayName("Tipo Caso")]
        public string TipoCaso { get; set; }
        [DisplayName("Título del Caso")]
        public string TituloCaso { get; set; }
        [DisplayName("Descrición")]
        public string Descripcion { get; set; }
        [DisplayName("Pruebas del Caso")]
        public string PruebasCaso { get; set; }
        [DisplayName("Fecha Inicio")]
        public DateTime FechaInicio { get; set; }
        [DisplayName("Fecha Final")]
        public DateTime FechaFin { get; set; }
        public string Cliente { get; set; }
        public int edad { get; set; }


        public string Abogado { get; set; }

        [DisplayName("Licencia de Abogado")]
        public  int  AbogadoLicencia { get; set; }


        // -------------------------------------//
        //---------Etapa Proceso Legal//

        [DisplayName("Número de etapa")]
        public int NumEtapa { get; set; }
        [DisplayName("Caso")]
        public int NumCaso { get; set; }
        public DateTime Fecha { get; set; }
        [DisplayName("Descripción")]
        public string DescripcionCaso { get; set; }
        [DisplayName("Pruebas")]
        public string PruebasEtapas { get; set; }
    }
}
