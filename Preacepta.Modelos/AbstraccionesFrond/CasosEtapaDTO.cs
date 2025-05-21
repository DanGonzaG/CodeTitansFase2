using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class CasosEtapaDTO
    {
        public int IdEtapaPl { get; set; }
        
        public DateTime Fecha { get; set; }
       
        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;
       
        public int IdCaso { get; set; }

        public bool Activo { get; set; }
        
        public virtual TCaso IdCasoNavigation { get; set; } = null!;
        
        public virtual ICollection<TCasosEvidencia> TCasosEvidencia { get; set; } = new List<TCasosEvidencia>();
    }
}
}
