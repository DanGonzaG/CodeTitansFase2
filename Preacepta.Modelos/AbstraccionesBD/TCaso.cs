using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_Casos")]
public partial class TCaso
{
    [Key]
    [Column("Id_caso")]
    public int IdCaso { get; set; }

    [Column("Nombre")]
    public string Nombre { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    [Column("Id_TipoCaso")]
    public int IdTipoCaso { get; set; }

    public string Descripcion { get; set; } = null!;

    [Column("Id_Abogado")]
    public int IdAbogado { get; set; }

    [Column("Id_Cliente")]
    public int IdCliente { get; set; }

    public bool Activo { get; set; }

    [ForeignKey("IdAbogado")]
    [InverseProperty("TCasos")]
    public virtual TGeAbogado IdAbogadoNavigation { get; set; } = null!;

    [ForeignKey("IdCliente")]
    [InverseProperty("TCasos")]
    public virtual TGePersona IdClienteNavigation { get; set; } = null!;

    [ForeignKey("IdTipoCaso")]
    [InverseProperty("TCasos")]
    public virtual TCasosTipo IdTipoCasoNavigation { get; set; } = null!;

    [InverseProperty("IdCasoNavigation")]
    public virtual ICollection<TCasosEtapa> TCasosEtapas { get; set; } = new List<TCasosEtapa>();

    [InverseProperty("IdCasoNavigation")]
    public virtual ICollection<TCasosEvidencia> TCasosEvidencia { get; set; } = new List<TCasosEvidencia>();
}
