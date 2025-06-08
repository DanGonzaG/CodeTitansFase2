using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_CrCantones")]
public partial class TCrCantone
{
    [Key]
    [Column("idCanton")]
    public int IdCanton { get; set; }

    [Column("idProvincia")]
    public int IdProvincia { get; set; }

    [StringLength(100)]
    public string? NombreCanton { get; set; }

    [ForeignKey("IdProvincia")]
    [InverseProperty("TCrCantones")]
    public virtual TCrProvincia IdProvinciaNavigation { get; set; } = null!;

    [InverseProperty("IdCatonNavigation")]
    public virtual ICollection<TCrDistrito> TCrDistritos { get; set; } = new List<TCrDistrito>();
}
