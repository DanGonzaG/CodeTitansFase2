using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_Citas")]
public partial class TCita
{
    [Key]
    [Column("Id_Cita")]
    public int IdCita { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly Hora { get; set; }

    [Column("Id_TipoCita")]
    public int IdTipoCita { get; set; }

    public int Anfitrion { get; set; }

    public string? LinkVideo { get; set; }

    [ForeignKey("Anfitrion")]
    [InverseProperty("TCita")]
    public virtual TGeAbogado AnfitrionNavigation { get; set; } = null!;

    [ForeignKey("IdTipoCita")]
    [InverseProperty("TCita")]
    public virtual TCitasTipo IdTipoCitaNavigation { get; set; } = null!;

    [InverseProperty("IdCitaNavigation")]
    public virtual ICollection<TCitasCliente> TCitasClientes { get; set; } = new List<TCitasCliente>();
}
