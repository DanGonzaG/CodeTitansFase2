using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class DocsInscripcionVehiculoDTO
    {
        [DisplayName("ID del Documento")]
        [Required(ErrorMessage = "Debe indicar el ID del documento")]
        public int IdDocumento { get; set; }

        [DisplayName("Cédula del Cliente")]
        [Required(ErrorMessage = "Debe indicar la cédula del cliente")]
        public int CedulaCliente { get; set; }

        [DisplayName("Cédula del Abogado")]
        [Required(ErrorMessage = "Debe indicar la cédula del abogado")]
        public int CedulaAbogado { get; set; }

        [DisplayName("Marca del Vehículo")]
        public int MarcaVehiculo { get; set; }

        [DisplayName("Estilo del Vehículo")]
        public int EstiloVehiculo { get; set; }

        [DisplayName("Modelo del Vehículo")]
        public int ModeloVehiculo { get; set; }

        [DisplayName("Categoría")]
        [Required(ErrorMessage = "Debe ingresar la categoría")]
        [StringLength(100)]
        public string Categoria { get; set; } = null!;

        [DisplayName("Marca del Motor")]
        [Required(ErrorMessage = "Debe ingresar la marca del motor")]
        [StringLength(100)]
        public string MarcaMotor { get; set; } = null!;

        [DisplayName("Número de Motor")]
        [Required(ErrorMessage = "Debe ingresar el número de motor")]
        [StringLength(100)]
        public string NumeroMotor { get; set; } = null!;

        [DisplayName("Número de Serie del Chasis")]
        [Required(ErrorMessage = "Debe ingresar el número de serie del chasis")]
        [StringLength(100)]
        public string NumeroSerieChasis { get; set; } = null!;

        [DisplayName("VIN")]
        [Required(ErrorMessage = "Debe ingresar el VIN")]
        [StringLength(100)]
        public string Vin { get; set; } = null!;

        [DisplayName("Año")]
        public int Anio { get; set; }

        [DisplayName("Carrocería")]
        [Required(ErrorMessage = "Debe ingresar la carrocería")]
        [StringLength(100)]
        public string Carroceria { get; set; } = null!;

        [DisplayName("Peso Neto (kg)")]
        public decimal PesoNeto { get; set; }

        [DisplayName("Peso Bruto (kg)")]
        public decimal PesoBruto { get; set; }

        [DisplayName("Potencia (HP)")]
        public decimal Potencia { get; set; }

        [DisplayName("Color")]
        [StringLength(50)]
        public string Color { get; set; } = null!;

        [DisplayName("Capacidad")]
        public int Capacidad { get; set; }

        [DisplayName("Combustible")]
        [StringLength(50)]
        public string Combustible { get; set; } = null!;

        [DisplayName("Cilindraje")]
        [StringLength(50)]
        public string Cilindraje { get; set; } = null!;

        [DisplayName("Lugar de Firma")]
        public int LugarFirma { get; set; }

        [DisplayName("Fecha de Firma")]
        public DateOnly FechaFirma { get; set; }

        [DisplayName("Abogado")]
        public virtual TGeAbogado CedulaAbogadoNavigation { get; set; } = null!;

        [DisplayName("Cliente")]
        public virtual TGePersona CedulaClienteNavigation { get; set; } = null!;

        [DisplayName("Estilo del Vehículo")]
        public virtual TDocsTipoVehiculo EstiloVehiculoNavigation { get; set; } = null!;

        [DisplayName("Lugar de Firma")]
        public virtual TCrDistrito LugarFirmaNavigation { get; set; } = null!;

        [DisplayName("Marca del Vehículo")]
        public virtual TDocsMarcaVehiculo MarcaVehiculoNavigation { get; set; } = null!;
    }
}
