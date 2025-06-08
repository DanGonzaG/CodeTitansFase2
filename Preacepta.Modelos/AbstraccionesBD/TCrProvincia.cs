using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_CrProvincias")]
public partial class TCrProvincia
{
    [Key]
    public int IdProvincia { get; set; }

    [StringLength(100)]
    public string? NombreProvincia { get; set; }

    [InverseProperty("IdProvinciaNavigation")]
    public virtual ICollection<TCrCantone> TCrCantones { get; set; } = new List<TCrCantone>();

    [InverseProperty("ProvinciaNavigation")]
    public virtual ICollection<TDocsContratoPrestacionServicio> TDocsContratoPrestacionServicios { get; set; } = new List<TDocsContratoPrestacionServicio>();
}
