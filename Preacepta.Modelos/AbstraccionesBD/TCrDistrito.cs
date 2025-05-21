using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_CrDistritos")]
public partial class TCrDistrito
{
    [Key]
    [Column("idDistrito")]
    public int IdDistrito { get; set; }

    [Column("idCaton")]
    public int IdCaton { get; set; }

    [Column("nombreDistrito")]
    [StringLength(100)]
    public string NombreDistrito { get; set; } = null!;

    [ForeignKey("IdCaton")]
    [InverseProperty("TCrDistritos")]
    public virtual TCrCantone IdCatonNavigation { get; set; } = null!;

    [InverseProperty("DistritoFincaNavigation")]
    public virtual ICollection<TDocsCompraventaFinca> TDocsCompraventaFincaDistritoFincaNavigations { get; set; } = new List<TDocsCompraventaFinca>();

    [InverseProperty("LugarFirmaNavigation")]
    public virtual ICollection<TDocsCompraventaFinca> TDocsCompraventaFincaLugarFirmaNavigations { get; set; } = new List<TDocsCompraventaFinca>();

    [InverseProperty("CiudadFirmaNavigation")]
    public virtual ICollection<TDocsContratoPrestacionServicio> TDocsContratoPrestacionServicios { get; set; } = new List<TDocsContratoPrestacionServicio>();

    [InverseProperty("LugarFirmaNavigation")]
    public virtual ICollection<TDocsInscripcionVehiculo> TDocsInscripcionVehiculos { get; set; } = new List<TDocsInscripcionVehiculo>();

    [InverseProperty("LugarFirmaNavigation")]
    public virtual ICollection<TDocsOpcionCompraventaVehiculo> TDocsOpcionCompraventaVehiculos { get; set; } = new List<TDocsOpcionCompraventaVehiculo>();

    [InverseProperty("LugarPagoNavigation")]
    public virtual ICollection<TDocsPagare> TDocsPagares { get; set; } = new List<TDocsPagare>();

    [InverseProperty("Direccion1Navigation")]
    public virtual ICollection<TGePersona> TGePersonas { get; set; } = new List<TGePersona>();
}
