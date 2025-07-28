using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_GeAbogados")]
public partial class TGeAbogado
{
    public int Carnet { get; set; }

    [Key]
    public int Cedula { get; set; }

    [Column("Id_TipoAbogado")]
    public int IdTipoAbogado { get; set; }

    [Column("C_Juridica")]
    public int CJuridica { get; set; }

    [ForeignKey("CJuridica")]
    [InverseProperty("TGeAbogados")]
    public virtual TGeNegocio CJuridicaNavigation { get; set; } = null!;

    [ForeignKey("Cedula")]
    [InverseProperty("TGeAbogado")]
    public virtual TGePersona CedulaNavigation { get; set; } = null!;

    [ForeignKey("IdTipoAbogado")]
    [InverseProperty("TGeAbogados")]
    public virtual TGeAbogadoTipo IdTipoAbogadoNavigation { get; set; } = null!;

    [InverseProperty("IdAbogadoNavigation")]
    public virtual ICollection<TCaso> TCasos { get; set; } = new List<TCaso>();

    [InverseProperty("AnfitrionNavigation")]
    public virtual ICollection<TCita> TCita { get; set; } = new List<TCita>();

    [InverseProperty("CedulaAbogadoNavigation")]
    public virtual ICollection<TDocsAutorizacionRevisionExpediente> TDocsAutorizacionRevisionExpedientes { get; set; } = new List<TDocsAutorizacionRevisionExpediente>();

    [InverseProperty("CedulaAbogadoNavigation")]
    public virtual ICollection<TDocsCompraventaFinca> TDocsCompraventaFincas { get; set; } = new List<TDocsCompraventaFinca>();

    [InverseProperty("CedulaAbogadoNavigation")]
    public virtual ICollection<TDocsContratoPrestacionServicio> TDocsContratoPrestacionServicios { get; set; } = new List<TDocsContratoPrestacionServicio>();

    [InverseProperty("CedulaAbogadoNavigation")]
    public virtual ICollection<TDocsInscripcionVehiculo> TDocsInscripcionVehiculos { get; set; } = new List<TDocsInscripcionVehiculo>();

    [InverseProperty("CedulaAbogadoNavigation")]
    public virtual ICollection<TDocsOpcionCompraventaVehiculo> TDocsOpcionCompraventaVehiculos { get; set; } = new List<TDocsOpcionCompraventaVehiculo>();

    [InverseProperty("IdAbogadoNavigation")]
    public virtual ICollection<TDocsPoderesEspecialesJudiciale> TDocsPoderesEspecialesJudiciales { get; set; } = new List<TDocsPoderesEspecialesJudiciale>();

    [InverseProperty("CedulaNavigation")]
    public virtual ICollection<TGeRedesSociale> TGeRedesSociales { get; set; } = new List<TGeRedesSociale>();
}
