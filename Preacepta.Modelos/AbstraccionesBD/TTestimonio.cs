using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_Testimonios")]
public partial class TTestimonio
{
    [Key]
    [Column("Id_Testimonio")]
    public int IdTestimonio { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    [Column("Id_Cliente")]
    public int IdCliente { get; set; }

    [StringLength(500)]
    public string? Comentario { get; set; }

    public int? Evaluacion { get; set; }

    public bool Activo { get; set; }

    [ForeignKey("IdCliente")]
    [InverseProperty("TTestimonios")]
    public virtual TGePersona IdClienteNavigation { get; set; } = null!;
}
