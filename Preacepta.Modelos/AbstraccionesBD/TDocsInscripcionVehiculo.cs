using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

namespace Preacepta.Modelos.AbstraccionesBD;

[Table("T_DocsInscripcionVehiculo")]
public partial class TDocsInscripcionVehiculo
{
    [Key]
    [Column("ID_Documento")]
    public int IdDocumento { get; set; }

    [Column("cedula_cliente")]
    public int CedulaCliente { get; set; }

    [Column("cedula_abogado")]
    public int CedulaAbogado { get; set; }

    [Column("marca_vehiculo")]
    public int MarcaVehiculo { get; set; }

    [Column("estilo_vehiculo")]
    public int EstiloVehiculo { get; set; }

    [Column("modelo_vehiculo")]
    public int ModeloVehiculo { get; set; }

    [Column("categoria")]
    [StringLength(100)]
    //[Unicode(false)]
    public string Categoria { get; set; } = null!;

    [Column("marca_motor")]
    [StringLength(100)]
    //[Unicode(false)]
    public string MarcaMotor { get; set; } = null!;

    [Column("numero_motor")]
    [StringLength(100)]
    //[Unicode(false)]
    public string NumeroMotor { get; set; } = null!;

    [Column("numero_serie_chasis")]
    [StringLength(100)]
    //[Unicode(false)]
    public string NumeroSerieChasis { get; set; } = null!;

    [Column("vin")]
    [StringLength(100)]
    //[Unicode(false)]
    public string Vin { get; set; } = null!;

    [Column("anio")]
    public int Anio { get; set; }

    [Column("carroceria")]
    [StringLength(100)]
    //[Unicode(false)]
    public string Carroceria { get; set; } = null!;

    [Column("peso_neto", TypeName = "decimal(10, 2)")]
    public decimal PesoNeto { get; set; }

    [Column("peso_bruto", TypeName = "decimal(10, 2)")]
    public decimal PesoBruto { get; set; }

    [Column("potencia", TypeName = "decimal(10, 2)")]
    public decimal Potencia { get; set; }

    [Column("color")]
    [StringLength(50)]
    //[Unicode(false)]
    public string Color { get; set; } = null!;

    [Column("capacidad")]
    public int Capacidad { get; set; }

    [Column("combustible")]
    [StringLength(50)]
    //[Unicode(false)]
    public string Combustible { get; set; } = null!;

    [Column("cilindraje")]
    [StringLength(50)]
    //[Unicode(false)]
    public string Cilindraje { get; set; } = null!;

    [Column("lugar_firma")]
    public int LugarFirma { get; set; }

    [Column("fecha_firma")]
    public DateOnly FechaFirma { get; set; }

    [ForeignKey("CedulaAbogado")]
    [InverseProperty("TDocsInscripcionVehiculos")]
    public virtual TGeAbogado CedulaAbogadoNavigation { get; set; } = null!;

    [ForeignKey("CedulaCliente")]
    [InverseProperty("TDocsInscripcionVehiculos")]
    public virtual TGePersona CedulaClienteNavigation { get; set; } = null!;

    [ForeignKey("EstiloVehiculo")]
    [InverseProperty("TDocsInscripcionVehiculos")]
    public virtual TDocsTipoVehiculo EstiloVehiculoNavigation { get; set; } = null!;

    [ForeignKey("LugarFirma")]
    [InverseProperty("TDocsInscripcionVehiculos")]
    public virtual TCrDistrito LugarFirmaNavigation { get; set; } = null!;

    [ForeignKey("MarcaVehiculo")]
    [InverseProperty("TDocsInscripcionVehiculos")]
    public virtual TDocsMarcaVehiculo MarcaVehiculoNavigation { get; set; } = null!;
}
