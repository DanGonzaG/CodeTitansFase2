using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_CitasClientes")]
public partial class TCitasCliente
{
    [Key]
    [Column("Id_CiCliente")]
    public int IdCiCliente { get; set; }

    [Column("Id_Cita")]
    public int IdCita { get; set; }

    [Column("Id_Cliente")]
    public int IdCliente { get; set; }

    [Column("UsuarioId")]
    [StringLength(450)]
    public string? UsuarioId { get; set; }

    [ForeignKey("IdCita")]
    [InverseProperty("TCitasClientes")]
    public virtual TCita IdCitaNavigation { get; set; } = null!;

    [ForeignKey("IdCliente")]
    [InverseProperty("TCitasClientes")]
    public virtual TGePersona IdClienteNavigation { get; set; } = null!;

}
