using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_GeNegocios")]
public partial class TGeNegocio
{
    [Key]
    [Column("C_Juridica")]
    public int CJuridica { get; set; }

    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [StringLength(50)]
    public string Telefono { get; set; } = null!;

    [StringLength(50)]
    public string Email { get; set; } = null!;

    [StringLength(100)]
    public string? Representante { get; set; }

    [Column("Fecha_Consolidacion")]
    public DateOnly FechaConsolidacion { get; set; }

    public int Direccion1 { get; set; }

    [StringLength(500)]
    public string Direccion2 { get; set; } = null!;

    [InverseProperty("CJuridicaNavigation")]
    public virtual ICollection<TGeAbogado> TGeAbogados { get; set; } = new List<TGeAbogado>();
}
