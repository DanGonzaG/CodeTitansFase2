using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_GePersonas")]
public partial class TGePersona
{
    [Key]
    [DisplayName("Cédula")]
    public int Cedula { get; set; }

    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [StringLength(50)]
    [DisplayName("Primer apellido")]
    public string Apellido1 { get; set; } = null!;

    [StringLength(50)]
    [DisplayName("Segundo Apellido")]
    public string Apellido2 { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public int Edad { get; set; }

    [StringLength(50)]
    public string EstadoCivil { get; set; } = null!;

    [StringLength(50)]
    public string Oficio { get; set; } = null!;

    public int Direccion1 { get; set; }

    [StringLength(500)]
    public string Direccion2 { get; set; } = null!;

    [StringLength(50)]
    public string Telefono1 { get; set; } = null!;

    [StringLength(50)]
    public string? Telefono2 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    public bool Activo { get; set; }

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(10)]
    public string Genero { get; set; } = null!;

    [ForeignKey("Direccion1")]
    [InverseProperty("TGePersonas")]
    public virtual TCrDistrito Direccion1Navigation { get; set; } = null!;

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<TCaso> TCasos { get; set; } = new List<TCaso>();

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<TCitasCliente> TCitasClientes { get; set; } = new List<TCitasCliente>();

    [InverseProperty("CedulaAsistenteNavigation")]
    public virtual ICollection<TDocsAutorizacionRevisionExpediente> TDocsAutorizacionRevisionExpedienteCedulaAsistenteNavigations { get; set; } = new List<TDocsAutorizacionRevisionExpediente>();

    [InverseProperty("CedulaImputadoNavigation")]
    public virtual ICollection<TDocsAutorizacionRevisionExpediente> TDocsAutorizacionRevisionExpedienteCedulaImputadoNavigations { get; set; } = new List<TDocsAutorizacionRevisionExpediente>();

    [InverseProperty("CedulaCompradorNavigation")]
    public virtual ICollection<TDocsCompraventaFinca> TDocsCompraventaFincaCedulaCompradorNavigations { get; set; } = new List<TDocsCompraventaFinca>();

    [InverseProperty("CedulaVendedorNavigation")]
    public virtual ICollection<TDocsCompraventaFinca> TDocsCompraventaFincaCedulaVendedorNavigations { get; set; } = new List<TDocsCompraventaFinca>();

    [InverseProperty("CedulaClienteNavigation")]
    public virtual ICollection<TDocsContratoPrestacionServicio> TDocsContratoPrestacionServicios { get; set; } = new List<TDocsContratoPrestacionServicio>();

    [InverseProperty("CedulaClienteNavigation")]
    public virtual ICollection<TDocsInscripcionVehiculo> TDocsInscripcionVehiculos { get; set; } = new List<TDocsInscripcionVehiculo>();

    [InverseProperty("CedulaCompradorNavigation")]
    public virtual ICollection<TDocsOpcionCompraventaVehiculo> TDocsOpcionCompraventaVehiculoCedulaCompradorNavigations { get; set; } = new List<TDocsOpcionCompraventaVehiculo>();

    [InverseProperty("CedulaPropietarioNavigation")]
    public virtual ICollection<TDocsOpcionCompraventaVehiculo> TDocsOpcionCompraventaVehiculoCedulaPropietarioNavigations { get; set; } = new List<TDocsOpcionCompraventaVehiculo>();

    [InverseProperty("CedulaDeudorNavigation")]
    public virtual ICollection<TDocsPagare> TDocsPagareCedulaDeudorNavigations { get; set; } = new List<TDocsPagare>();

    [InverseProperty("CedulaFiadorNavigation")]
    public virtual ICollection<TDocsPagare> TDocsPagareCedulaFiadorNavigations { get; set; } = new List<TDocsPagare>();

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<TDocsPoderesEspecialesJudiciale> TDocsPoderesEspecialesJudiciales { get; set; } = new List<TDocsPoderesEspecialesJudiciale>();

    [InverseProperty("CedulaNavigation")]
    public virtual TGeAbogado? TGeAbogado { get; set; }

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<TTestimonio> TTestimonios { get; set; } = new List<TTestimonio>();
}
