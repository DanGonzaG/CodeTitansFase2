using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class CasosTipoDTO
    {
        public int IdTipoCaso { get; set; }       
        public string Nombre { get; set; } = null!;        
        public virtual ICollection<TCaso> TCasos { get; set; } = new List<TCaso>();
    }
}
