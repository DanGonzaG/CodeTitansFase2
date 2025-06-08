using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_DocsCompraventaFinca")]
public partial class TDocsCompraventaFinca
{
    [Key]
    [Column("ID_Documento")]
    public int IdDocumento { get; set; }

    [Column("numero_escritura")]
    [StringLength(50)]
    //[Unicode(false)]
    public string NumeroEscritura { get; set; } = null!;

    [Column("cedula_abogado")]
    public int CedulaAbogado { get; set; }

    [Column("cedula_vendedor")]
    public int CedulaVendedor { get; set; }

    [Column("cedula_comprador")]
    public int CedulaComprador { get; set; }

    [Column("monto_venta", TypeName = "decimal(15, 2)")]
    public decimal MontoVenta { get; set; }

    [Column("partido_finca")]
    [StringLength(50)]
    //[Unicode(false)]
    public string PartidoFinca { get; set; } = null!;

    [Column("matricula_finca")]
    [StringLength(50)]
    //[Unicode(false)]
    public string MatriculaFinca { get; set; } = null!;

    [Column("naturaleza_finca")]
    [StringLength(100)]
    //[Unicode(false)]
    public string NaturalezaFinca { get; set; } = null!;

    [Column("distrito_finca")]
    public int DistritoFinca { get; set; }

    [Column("canton_finca")]
    public int CantonFinca { get; set; }

    [Column("provincia_finca")]
    public int ProvinciaFinca { get; set; }

    [Column("area_finca_m2", TypeName = "decimal(10, 2)")]
    public decimal AreaFincaM2 { get; set; }

    [Column("plano_catastrado")]
    [StringLength(100)]
    //[Unicode(false)]
    public string PlanoCatastrado { get; set; } = null!;

    [Column("colinda_norte", TypeName = "text")]
    public string ColindaNorte { get; set; } = null!;

    [Column("colinda_sur", TypeName = "text")]
    public string ColindaSur { get; set; } = null!;

    [Column("colinda_este", TypeName = "text")]
    public string ColindaEste { get; set; } = null!;

    [Column("colinda_oeste", TypeName = "text")]
    public string ColindaOeste { get; set; } = null!;

    [Column("forma_pago")]
    [StringLength(100)]
    //[Unicode(false)]
    public string FormaPago { get; set; } = null!;

    [Column("medio_pago")]
    [StringLength(100)]
    //[Unicode(false)]
    public string MedioPago { get; set; } = null!;

    [Column("origen_fondos", TypeName = "text")]
    public string OrigenFondos { get; set; } = null!;

    [Column("lugar_firma")]
    public int LugarFirma { get; set; }

    [Column("hora_firma")]
    public TimeOnly HoraFirma { get; set; }

    [Column("fecha_firma")]
    public DateOnly FechaFirma { get; set; }

    [ForeignKey("CedulaAbogado")]
    [InverseProperty("TDocsCompraventaFincas")]
    public virtual TGeAbogado CedulaAbogadoNavigation { get; set; } = null!;

    [ForeignKey("CedulaComprador")]
    [InverseProperty("TDocsCompraventaFincaCedulaCompradorNavigations")]
    public virtual TGePersona CedulaCompradorNavigation { get; set; } = null!;

    [ForeignKey("CedulaVendedor")]
    [InverseProperty("TDocsCompraventaFincaCedulaVendedorNavigations")]
    public virtual TGePersona CedulaVendedorNavigation { get; set; } = null!;

    [ForeignKey("DistritoFinca")]
    [InverseProperty("TDocsCompraventaFincaDistritoFincaNavigations")]
    public virtual TCrDistrito DistritoFincaNavigation { get; set; } = null!;

    [ForeignKey("LugarFirma")]
    [InverseProperty("TDocsCompraventaFincaLugarFirmaNavigations")]
    public virtual TCrDistrito LugarFirmaNavigation { get; set; } = null!;
}
