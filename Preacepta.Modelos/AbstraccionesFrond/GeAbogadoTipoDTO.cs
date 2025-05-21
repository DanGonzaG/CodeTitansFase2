using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class GeAbogadoTipoDTO
    {
        public int IdTipoAbogado { get; set; }
       
        public string Nombre { get; set; } = null!;
        
        public virtual ICollection<TGeAbogado> TGeAbogados { get; set; } = new List<TGeAbogado>();
    }
}
