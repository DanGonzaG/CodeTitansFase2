using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_DocsMarcaVehiculos")]
public partial class TDocsMarcaVehiculo
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("MarcaVehiculoNavigation")]
    public virtual ICollection<TDocsInscripcionVehiculo> TDocsInscripcionVehiculos { get; set; } = new List<TDocsInscripcionVehiculo>();

    [InverseProperty("MarcaMotorNavigation")]
    public virtual ICollection<TDocsOpcionCompraventaVehiculo> TDocsOpcionCompraventaVehiculoMarcaMotorNavigations { get; set; } = new List<TDocsOpcionCompraventaVehiculo>();

    [InverseProperty("MarcaVehiculoNavigation")]
    public virtual ICollection<TDocsOpcionCompraventaVehiculo> TDocsOpcionCompraventaVehiculoMarcaVehiculoNavigations { get; set; } = new List<TDocsOpcionCompraventaVehiculo>();
}
