using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_GeAbogadoTipo")]
public partial class TGeAbogadoTipo
{
    [Key]
    [Column("Id_TipoAbogado")]
    public int IdTipoAbogado { get; set; }

    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdTipoAbogadoNavigation")]
    public virtual ICollection<TGeAbogado> TGeAbogados { get; set; } = new List<TGeAbogado>();
}
