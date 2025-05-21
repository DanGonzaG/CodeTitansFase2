using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class GeAbogadoDTO
    {
        public int Cedula { get; set; }
       
        public int IdTipoAbogado { get; set; }
        
        public int CJuridica { get; set; }
      
        public virtual TGeNegocio CJuridicaNavigation { get; set; } = null!;
      
        public virtual TGePersona CedulaNavigation { get; set; } = null!;

        public virtual TGeAbogadoTipo IdTipoAbogadoNavigation { get; set; } = null!;
       
        public virtual ICollection<TCaso> TCasos { get; set; } = new List<TCaso>();
        
        public virtual ICollection<TCita> TCita { get; set; } = new List<TCita>();
        
        public virtual ICollection<TDocsAutorizacionRevisionExpediente> TDocsAutorizacionRevisionExpedientes { get; set; } = new List<TDocsAutorizacionRevisionExpediente>();
       
        public virtual ICollection<TDocsCompraventaFinca> TDocsCompraventaFincas { get; set; } = new List<TDocsCompraventaFinca>();
       
        public virtual ICollection<TDocsContratoPrestacionServicio> TDocsContratoPrestacionServicios { get; set; } = new List<TDocsContratoPrestacionServicio>();
       
        public virtual ICollection<TDocsInscripcionVehiculo> TDocsInscripcionVehiculos { get; set; } = new List<TDocsInscripcionVehiculo>();
       
        public virtual ICollection<TDocsOpcionCompraventaVehiculo> TDocsOpcionCompraventaVehiculos { get; set; } = new List<TDocsOpcionCompraventaVehiculo>();
       
        public virtual ICollection<TDocsPoderesEspecialesJudiciale> TDocsPoderesEspecialesJudiciales { get; set; } = new List<TDocsPoderesEspecialesJudiciale>();
        
        public virtual ICollection<TGeRedesSociale> TGeRedesSociales { get; set; } = new List<TGeRedesSociale>();
    }
}
