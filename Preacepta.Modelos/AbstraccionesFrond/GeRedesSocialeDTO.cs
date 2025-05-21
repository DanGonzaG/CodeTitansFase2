using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class GeRedesSocialeDTO
    {
        public int IdRs { get; set; }

        public int Cedula { get; set; }

        public string LinkRedSocila { get; set; } = null!;
        
        public virtual TGeAbogado CedulaNavigation { get; set; } = null!;
    }
}
