using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_DocsPoderesEspecialesJudiciales")]
public partial class TDocsPoderesEspecialesJudiciale
{
    [Key]
    [Column("Id_doc")]
    public int IdDoc { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    [Column("Id_Abogado")]
    public int IdAbogado { get; set; }

    [Column("Id_Cliente")]
    public int IdCliente { get; set; }

    public string Texto { get; set; } = null!;

    [ForeignKey("IdAbogado")]
    [InverseProperty("TDocsPoderesEspecialesJudiciales")]
    public virtual TGeAbogado IdAbogadoNavigation { get; set; } = null!;

    [ForeignKey("IdCliente")]
    [InverseProperty("TDocsPoderesEspecialesJudiciales")]
    public virtual TGePersona IdClienteNavigation { get; set; } = null!;
}
