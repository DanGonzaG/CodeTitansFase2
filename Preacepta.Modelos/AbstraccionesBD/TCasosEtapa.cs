using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_CasosEtapas")]
public partial class TCasosEtapa
{
    [Key]
    [Column("Id_EtapaPL")]
    public int IdEtapaPl { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    [Column("Id_Caso")]
    public int IdCaso { get; set; }

    public bool Activo { get; set; }

    [ForeignKey("IdCaso")]
    [InverseProperty("TCasosEtapas")]
    public virtual TCaso IdCasoNavigation { get; set; } = null!;

    [InverseProperty("IdCaso1")]
    public virtual ICollection<TCasosEvidencia> TCasosEvidencia { get; set; } = new List<TCasosEvidencia>();
}
