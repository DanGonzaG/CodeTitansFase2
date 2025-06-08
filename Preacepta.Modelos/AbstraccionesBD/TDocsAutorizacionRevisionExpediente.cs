using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_DocsAutorizacionRevisionExpediente")]
public partial class TDocsAutorizacionRevisionExpediente
{
    [Key]
    [Column("ID_Documento")]
    public int IdDocumento { get; set; }

    [Column("expediente")]
    [StringLength(50)]
    //[Unicode(false)]
    public string Expediente { get; set; } = null!;

    [Column("delito")]
    [StringLength(100)]
    //[Unicode(false)]
    public string Delito { get; set; } = null!;

    [Column("cedula_imputado")]
    public int CedulaImputado { get; set; }

    [Column("ofendido")]
    [StringLength(150)]
    //[Unicode(false)]
    public string Ofendido { get; set; } = null!;

    [Column("cedula_abogado")]
    public int CedulaAbogado { get; set; }

    [Column("cedula_asistente")]
    public int CedulaAsistente { get; set; }

    [ForeignKey("CedulaAbogado")]
    [InverseProperty("TDocsAutorizacionRevisionExpedientes")]
    public virtual TGeAbogado CedulaAbogadoNavigation { get; set; } = null!;

    [ForeignKey("CedulaAsistente")]
    [InverseProperty("TDocsAutorizacionRevisionExpedienteCedulaAsistenteNavigations")]
    public virtual TGePersona CedulaAsistenteNavigation { get; set; } = null!;

    [ForeignKey("CedulaImputado")]
    [InverseProperty("TDocsAutorizacionRevisionExpedienteCedulaImputadoNavigations")]
    public virtual TGePersona CedulaImputadoNavigation { get; set; } = null!;
}
