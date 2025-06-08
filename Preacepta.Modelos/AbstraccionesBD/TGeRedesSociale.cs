using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_GeRedesSociales")]
public partial class TGeRedesSociale
{
    [Key]
    [Column("Id_Rs")]
    public int IdRs { get; set; }

    public int Cedula { get; set; }

    public string LinkRedSocila { get; set; } = null!;

    [ForeignKey("Cedula")]
    [InverseProperty("TGeRedesSociales")]
    public virtual TGeAbogado CedulaNavigation { get; set; } = null!;
}
