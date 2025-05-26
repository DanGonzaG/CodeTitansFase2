using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class GeNegocioDTO
    {
        [DisplayName("Cédula Jurídica")]
        public int CJuridica { get; set; }
        
        public string Nombre { get; set; } = null!;

        [DisplayName("Teléfono")]
        public string Telefono { get; set; } = null!;
       
        public string Email { get; set; } = null!;
        
        public string? Representante { get; set; }

        [DisplayName("Fecha de Creación")]
        public string FechaConsolidacion { get; set; }

        [DisplayName("Ubicacion")]
        public int Direccion1 { get; set; }

        [DisplayName("Direccion exacta")]
        public string Direccion2 { get; set; } = null!;
       
        public virtual ICollection<TGeAbogado> TGeAbogados { get; set; } = new List<TGeAbogado>();
    }
}
