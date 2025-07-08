using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_DocsPagare")]
public partial class TDocsPagare
{
    [Key]
    [Column("ID_Documento")]
    public int IdDocumento { get; set; }

    [Column("monto_numerico", TypeName = "decimal(15, 2)")]
    public decimal MontoNumerico { get; set; }

    [Column("cedula_deudor")]
    public int CedulaDeudor { get; set; }

    [Column("sociedad_deudor")]
    [StringLength(150)]
    //[Unicode(false)]
    public string SociedadDeudor { get; set; } = null!;

    [Column("cedula_juridica_sociedad")]
    [StringLength(20)]
    //[Unicode(false)]
    public string CedulaJuridicaSociedad { get; set; } = null!;

    [Column("acreedor_nombre")]
    [StringLength(150)]
    //[Unicode(false)]
    public string AcreedorNombre { get; set; } = null!;

    [Column("cedula_juridica_acreedor")]
    [StringLength(20)]
    //[Unicode(false)]
    public string CedulaJuridicaAcreedor { get; set; } = null!;

    [Column("acreedor_domicilio", TypeName = "text")]
    public string AcreedorDomicilio { get; set; } = null!;

    [Column("fecha_firma")]
    public DateOnly FechaFirma { get; set; }

    [Column("hora_firma")]
    public TimeOnly HoraFirma { get; set; }

    [Column("fecha_vencimiento")]
    public DateOnly FechaVencimiento { get; set; }

    [Column("interes_formula", TypeName = "text")]
    public string InteresFormula { get; set; } = null!;

    [Column("interes_tasa_actual", TypeName = "decimal(5, 2)")]
    public decimal InteresTasaActual { get; set; }

    [Column("interes_base")]
    [StringLength(100)]
    //[Unicode(false)]
    public string InteresBase { get; set; } = null!;

    [Column("lugar_pago")]
    public int LugarPago { get; set; }

    [Column("cedula_fiador")]
    public int CedulaFiador { get; set; }

    [Column("ubicacion_firma")]
    public int UbicacionFirma { get; set; }

    [ForeignKey("CedulaDeudor")]
    [InverseProperty("TDocsPagareCedulaDeudorNavigations")]
    public virtual TGePersona? CedulaDeudorNavigation { get; set; } = null!;

    [ForeignKey("CedulaFiador")]
    [InverseProperty("TDocsPagareCedulaFiadorNavigations")]
    public virtual TGePersona? CedulaFiadorNavigation { get; set; } = null!;

    [ForeignKey("LugarPago")]
    [InverseProperty("TDocsPagares")]
    public virtual TCrDistrito? LugarPagoNavigation { get; set; } = null!;
}
