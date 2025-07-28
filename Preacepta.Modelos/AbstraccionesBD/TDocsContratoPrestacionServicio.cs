using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_DocsContratoPrestacionServicios")]
public partial class TDocsContratoPrestacionServicio
{
    [Key]
    [Column("ID_Documento")]
    public int IdDocumento { get; set; }

    [Column("razon_social_empresa")]
    [StringLength(255)]
    //[Unicode(false)]
    public string RazonSocialEmpresa { get; set; } = null!;

    [Column("provincia")]
    public int Provincia { get; set; }

    [Column("cedula_juridica_empresa")]
    [StringLength(20)]
    //[Unicode(false)]
    public string CedulaJuridicaEmpresa { get; set; } = null!;

    [Column("cedula_abogado")]
    public int CedulaAbogado { get; set; }

    [Column("cedula_cliente")]
    public int CedulaCliente { get; set; }

    [Column("tipo_servicios")]
    [StringLength(255)]
    //[Unicode(false)]
    public string TipoServicios { get; set; } = null!;

    [Column("fecha_inicio")]
    public DateOnly FechaInicio { get; set; }

    [Column("fecha_final")]
    public DateOnly FechaFinal { get; set; }

    [Column("monto_honorarios", TypeName = "decimal(10, 2)")]
    public decimal MontoHonorarios { get; set; }

    [Column("informacion_confidencial", TypeName = "text")]
    public string InformacionConfidencial { get; set; } = null!;

    [Column("ciudad_firma")]
    public int CiudadFirma { get; set; }

    [Column("hora_firma")]
    public TimeOnly HoraFirma { get; set; }

    [Column("fecha_firma")]
    public DateOnly FechaFirma { get; set; }

    [ForeignKey("CedulaAbogado")]
    [InverseProperty("TDocsContratoPrestacionServicios")]
    public virtual TGeAbogado CedulaAbogadoNavigation { get; set; } = null!;

    [ForeignKey("CedulaCliente")]
    [InverseProperty("TDocsContratoPrestacionServicios")]
    public virtual TGePersona CedulaClienteNavigation { get; set; } = null!;

    [ForeignKey("CiudadFirma")]
    [InverseProperty("TDocsContratoPrestacionServicios")]
    public virtual TCrDistrito CiudadFirmaNavigation { get; set; } = null!;

    [ForeignKey("Provincia")]
    [InverseProperty("TDocsContratoPrestacionServicios")]
    public virtual TCrProvincia ProvinciaNavigation { get; set; } = null!;
}
