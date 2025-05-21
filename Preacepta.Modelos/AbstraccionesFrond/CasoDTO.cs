using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class CasoDTO
    {
        public int IdCaso { get; set; }
        
        public DateTime Fecha { get; set; }
       
        public int IdTipoCaso { get; set; }

        public string Descripcion { get; set; } = null!;
       
        public int IdAbogado { get; set; }

        
        public int IdCliente { get; set; }

        public bool Activo { get; set; }
       
        public virtual TGeAbogado IdAbogadoNavigation { get; set; } = null!;
       
        public virtual TGePersona IdClienteNavigation { get; set; } = null!;
       
        public virtual TCasosTipo IdTipoCasoNavigation { get; set; } = null!;

        public virtual ICollection<TCasosEtapa> TCasosEtapas { get; set; } = new List<TCasosEtapa>();

        public virtual ICollection<TCasosEvidencia> TCasosEvidencia { get; set; } = new List<TCasosEvidencia>();
    }
}
