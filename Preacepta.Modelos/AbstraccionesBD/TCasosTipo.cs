using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_CasosTipos")]
public partial class TCasosTipo
{
    [Key]
    [Column("Id_TipoCaso")]
    public int IdTipoCaso { get; set; }

    [StringLength(200)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdTipoCasoNavigation")]
    public virtual ICollection<TCaso> TCasos { get; set; } = new List<TCaso>();
}
