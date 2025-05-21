using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class GeNegocioDTO
    {
        public int CJuridica { get; set; }
        
        public string Nombre { get; set; } = null!;
       
        public string Telefono { get; set; } = null!;
       
        public string Email { get; set; } = null!;
        
        public string? Representante { get; set; }
       
        public DateOnly FechaConsolidacion { get; set; }

        public int Direccion1 { get; set; }
        
        public string Direccion2 { get; set; } = null!;
       
        public virtual ICollection<TGeAbogado> TGeAbogados { get; set; } = new List<TGeAbogado>();
    }
}
