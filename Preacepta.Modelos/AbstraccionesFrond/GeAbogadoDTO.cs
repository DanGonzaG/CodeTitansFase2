using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class GeAbogadoDTO
    {
        [Required(ErrorMessage = "Debe de agregar el carnet del abogado")]
        public int Carnet { get; set; }

        [Required(ErrorMessage = "Debe de agregar la cédula del abogado")]
        [DisplayName("Cédula")]
        public int Cedula { get; set; }

        [Required(ErrorMessage = "Debe de seleccionar la especialidad del abogado")]
        [DisplayName("Tipo de abogado")]
        public int IdTipoAbogado { get; set; }

        [DisplayName("Cedula Jurídica")]
        public int CJuridica { get; set; }
        [DisplayName("Despacho")]
        public virtual TGeNegocio? CJuridicaNavigation { get; set; } = null!;
        [DisplayName("Funcionario")]
        public virtual TGePersona? CedulaNavigation { get; set; } = null!;
        [DisplayName("Especialidad")]
        public virtual TGeAbogadoTipo? IdTipoAbogadoNavigation { get; set; } = null!;

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
