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

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Marca del Vehículo")]
        public int MarcaVehiculo { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Estilo del Vehículo")]
        public int EstiloVehiculo { get; set; }

        [DisplayName("Modelo del Vehículo")]
        public int ModeloVehiculo { get; set; }

        [DisplayName("Categoría")]
        [Required(ErrorMessage = "Debe ingresar la categoría")]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string Categoria { get; set; } = null!;

        [DisplayName("Marca del Motor")]
        [Required(ErrorMessage = "Debe ingresar la marca del motor")]
        [StringLength(100)]
        public string MarcaMotor { get; set; } = null!;

        [DisplayName("Número de Motor")]
        [Required(ErrorMessage = "Debe ingresar el número de motor")]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string NumeroMotor { get; set; } = null!;

        [DisplayName("Número de Serie del Chasis")]
        [Required(ErrorMessage = "Debe ingresar el número de serie del chasis")]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string NumeroSerieChasis { get; set; } = null!;

        [DisplayName("VIN")]
        [Required(ErrorMessage = "Debe ingresar el VIN")]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string Vin { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Año")]
        public int Anio { get; set; }

        [DisplayName("Carrocería")]
        [Required(ErrorMessage = "Debe ingresar la carrocería")]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string Carroceria { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Peso Neto (kg)")]
        public decimal PesoNeto { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Peso Bruto (kg)")]
        public decimal PesoBruto { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Potencia (HP)")]
        public decimal Potencia { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Color")]
        [MaxLength(50, ErrorMessage = "El nombre no debe exceder los 50 caracteres")]
        public string Color { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Capacidad")]
        public int Capacidad { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Combustible")]
        [MaxLength(50, ErrorMessage = "El nombre no debe exceder los 50 caracteres")]
        public string Combustible { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Cilindraje")]
        [MaxLength(50, ErrorMessage = "El nombre no debe exceder los 50 caracteres")]
        public string Cilindraje { get; set; } = null!;


        [DisplayName("Lugar de Firma")]
        public int LugarFirma { get; set; }

        [DisplayName("Fecha de Firma")]
        [DataType(DataType.Date)]
        public DateOnly FechaFirma { get; set; }

        [DisplayName("Abogado")]
        public virtual TGeAbogado? CedulaAbogadoNavigation { get; set; } = null!;

        [DisplayName("Cliente")]
        public virtual TGePersona? CedulaClienteNavigation { get; set; } = null!;

        [DisplayName("Estilo del Vehículo")]
        public virtual TDocsTipoVehiculo? EstiloVehiculoNavigation { get; set; } = null!;

        [DisplayName("Lugar de Firma")]
        public virtual TCrDistrito? LugarFirmaNavigation { get; set; } = null!;

        [DisplayName("Marca del Vehículo")]
        public virtual TDocsMarcaVehiculo? MarcaVehiculoNavigation { get; set; } = null!;
    }
}
