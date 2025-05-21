using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class CasosEvidenciaDTO
    {
        public int IdEvidencia { get; set; }
       
        public string Titulo { get; set; } = null!;
       
        public int IdCaso { get; set; }

        public string? Archivo { get; set; }
        
        public virtual TCasosEtapa IdCaso1 { get; set; } = null!;
        
        public virtual TCaso IdCasoNavigation { get; set; } = null!;
    }
}
