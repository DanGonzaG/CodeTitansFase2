using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_DocsOpcionCompraventaVehiculo")]
public partial class TDocsOpcionCompraventaVehiculo
{
    [Key]
    [Column("ID_Documento")]
    public int IdDocumento { get; set; }

    [Column("numero_escritura")]
    [StringLength(50)]
    //[Unicode(false)]
    public string NumeroEscritura { get; set; } = null!;

    [Column("cedula_abogado")]
    public int CedulaAbogado { get; set; }

    [Column("cedula_propietario")]
    public int CedulaPropietario { get; set; }

    [Column("cedula_comprador")]
    public int CedulaComprador { get; set; }

    [Column("placa_vehiculo")]
    [StringLength(20)]
    //[Unicode(false)]
    public string PlacaVehiculo { get; set; } = null!;

    [Column("marca_vehiculo")]
    public int MarcaVehiculo { get; set; }

    [Column("tipo_vehiculo")]
    public int TipoVehiculo { get; set; }

    [Column("modelo_vehiculo")]
    [StringLength(100)]
    //[Unicode(false)]
    public string ModeloVehiculo { get; set; } = null!;

    [Column("carroceria")]
    [StringLength(100)]
    //[Unicode(false)]
    public string Carroceria { get; set; } = null!;

    [Column("categoria")]
    [StringLength(100)]
    //[Unicode(false)]
    public string Categoria { get; set; } = null!;

    [Column("chasis")]
    [StringLength(100)]
    //[Unicode(false)]
    public string Chasis { get; set; } = null!;

    [Column("serie")]
    [StringLength(100)]
    //[Unicode(false)]
    public string Serie { get; set; } = null!;

    [Column("vin")]
    [StringLength(100)]
    //[Unicode(false)]
    public string Vin { get; set; } = null!;

    [Column("marca_motor")]
    public int MarcaMotor { get; set; }

    [Column("numero_motor")]
    [StringLength(100)]
    //Unicode(false)]
    public string NumeroMotor { get; set; } = null!;

    [Column("color")]
    [StringLength(50)]
    //[Unicode(false)]
    public string Color { get; set; } = null!;

    [Column("combustible")]
    public int Combustible { get; set; }

    [Column("anio")]
    public int Anio { get; set; }

    [Column("capacidad")]
    [StringLength(50)]
    //[Unicode(false)]
    public string Capacidad { get; set; } = null!;

    [Column("cilindraje")]
    [StringLength(50)]
    //[Unicode(false)]
    public string Cilindraje { get; set; } = null!;

    [Column("precio", TypeName = "decimal(15, 2)")]
    public decimal Precio { get; set; }

    [Column("moneda_precio")]
    [StringLength(10)]
    //[Unicode(false)]
    public string MonedaPrecio { get; set; } = null!;

    [Column("plazo_opcion_anios")]
    public int PlazoOpcionAnios { get; set; }

    [Column("fecha_inicio")]
    public DateOnly FechaInicio { get; set; }

    [Column("monto_senal", TypeName = "decimal(15, 2)")]
    public decimal MontoSenal { get; set; }

    [Column("moneda_senal")]
    [StringLength(10)]
    //[Unicode(false)]
    public string MonedaSenal { get; set; } = null!;

    [Column("monto_a_devolver", TypeName = "decimal(15, 2)")]
    public decimal MontoADevolver { get; set; }

    [Column("monto_a_perder", TypeName = "decimal(15, 2)")]
    public decimal MontoAPerder { get; set; }

    [Column("moneda_monto_perdido")]
    [StringLength(10)]
    //[Unicode(false)]
    public string MonedaMontoPerdido { get; set; } = null!;

    [Column("gastos_traspaso_pagados_por")]
    [StringLength(150)]
    //[Unicode(false)]
    public string GastosTraspasoPagadosPor { get; set; } = null!;

    [Column("lugar_firma")]
    public int LugarFirma { get; set; }

    [Column("hora_firma")]
    public TimeOnly HoraFirma { get; set; }

    [Column("fecha_firma")]
    public DateOnly FechaFirma { get; set; }

    [ForeignKey("CedulaAbogado")]
    [InverseProperty("TDocsOpcionCompraventaVehiculos")]
    public virtual TGeAbogado CedulaAbogadoNavigation { get; set; } = null!;

    [ForeignKey("CedulaComprador")]
    [InverseProperty("TDocsOpcionCompraventaVehiculoCedulaCompradorNavigations")]
    public virtual TGePersona CedulaCompradorNavigation { get; set; } = null!;

    [ForeignKey("CedulaPropietario")]
    [InverseProperty("TDocsOpcionCompraventaVehiculoCedulaPropietarioNavigations")]
    public virtual TGePersona CedulaPropietarioNavigation { get; set; } = null!;

    [ForeignKey("Combustible")]
    [InverseProperty("TDocsOpcionCompraventaVehiculos")]
    public virtual TDocsCombustible CombustibleNavigation { get; set; } = null!;

    [ForeignKey("LugarFirma")]
    [InverseProperty("TDocsOpcionCompraventaVehiculos")]
    public virtual TCrDistrito LugarFirmaNavigation { get; set; } = null!;

    [ForeignKey("MarcaMotor")]
    [InverseProperty("TDocsOpcionCompraventaVehiculoMarcaMotorNavigations")]
    public virtual TDocsMarcaVehiculo MarcaMotorNavigation { get; set; } = null!;

    [ForeignKey("MarcaVehiculo")]
    [InverseProperty("TDocsOpcionCompraventaVehiculoMarcaVehiculoNavigations")]
    public virtual TDocsMarcaVehiculo MarcaVehiculoNavigation { get; set; } = null!;

    [ForeignKey("TipoVehiculo")]
    [InverseProperty("TDocsOpcionCompraventaVehiculos")]
    public virtual TDocsTipoVehiculo TipoVehiculoNavigation { get; set; } = null!;
}
