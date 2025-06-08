using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_CasosEvidencias")]
public partial class TCasosEvidencia
{
    [Key]
    [Column("Id_Evidencia")]
    public int IdEvidencia { get; set; }

    [StringLength(100)]
    public string Titulo { get; set; } = null!;

    [Column("Id_caso")]
    public int IdCaso { get; set; }

    public string? Archivo { get; set; }

    [ForeignKey("IdCaso")]
    [InverseProperty("TCasosEvidencia")]
    public virtual TCasosEtapa IdCaso1 { get; set; } = null!;

    [ForeignKey("IdCaso")]
    [InverseProperty("TCasosEvidencia")]
    public virtual TCaso IdCasoNavigation { get; set; } = null!;
}
