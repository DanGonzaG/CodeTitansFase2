using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class DocsOpcionCompraventaVehiculoDTO
    {
        [DisplayName("Id del documento")]
        [Required(ErrorMessage = "Dato requerido")]
        public int IdDocumento { get; set; }

        [DisplayName("Numero de escritura")]
        [MaxLength(50, ErrorMessage = "Capacidad del contenido excedida")]
        [Required(ErrorMessage = "El numero de escritura es requerido")]
        public string NumeroEscritura { get; set; } = null!;

        [DisplayName("Cedula del abogado")]
        public int CedulaAbogado { get; set; }

        [DisplayName("Cedula del propietario")]
        public int CedulaPropietario { get; set; }

        [DisplayName("Cedula del comprador")]
        public int CedulaComprador { get; set; }

        [DisplayName("Placa del vehiculo")]
        [MaxLength(20, ErrorMessage = "Capacidad de la placa excedida")]
        [Required(ErrorMessage = "Debe ingresar un placa para el vehiculo")]
        public string PlacaVehiculo { get; set; } = null!;

        [DisplayName("Marca del vehiculo")]
        [Required(ErrorMessage = "Debe de seleccionar una marca para el vehiculo")]
        public int MarcaVehiculo { get; set; }

        [DisplayName("Tipo de vehiculo")]
        [Required(ErrorMessage = "Debe de seleccionar un tipo de vehiculo")]
        public int TipoVehiculo { get; set; }

        [DisplayName("Modelo del vehiculo")]
        [MaxLength(100, ErrorMessage = "Capacidad excedida")]
        [Required(ErrorMessage = "Debe de escribir el modelo del vehiculo")]
        public string ModeloVehiculo { get; set; } = null!;

        [MaxLength(100, ErrorMessage = "Capacidad excedida")]
        [Required(ErrorMessage = "Debe de ingresar el tipo de carroceria del vehiculo")]
        public string Carroceria { get; set; } = null!;

        [MaxLength(100, ErrorMessage = "Debe ingresar una categoria")]
        [Required(ErrorMessage = "Debe de ingresar la categoria del vehiculo")]
        public string Categoria { get; set; } = null!;

        [MaxLength(100, ErrorMessage = "Capacidad excedida")]
        [Required(ErrorMessage = "Debe de ingresar el chasis del vehiculo")]
        public string Chasis { get; set; } = null!;

        [MaxLength(100, ErrorMessage = "Capacidad excedida")]
        [Required(ErrorMessage = "Debe de ingresar la serie del vehiculo")]
        public string Serie { get; set; } = null!;

        [MaxLength(100, ErrorMessage = "Capacidad excedida")]
        [Required(ErrorMessage = "Debe de ingresar el vin del vehiculo")]
        public string Vin { get; set; } = null!;

        [DisplayName("Marca del motor")]
        [Required(ErrorMessage = "Debe de seleccionar la marca del motor")]
        public int MarcaMotor { get; set; }

        [DisplayName("Numero del motor")]
        [MaxLength(100, ErrorMessage = "Capacidad excedida")]
        [Required(ErrorMessage = "Debe de ingresar el numero del motor")]
        public string NumeroMotor { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "Capacidad excedida")]
        [Required(ErrorMessage = "Debe de ingresar el color del vehiculo")]
        public string Color { get; set; } = null!;

        [Required(ErrorMessage = "Debe de seleccionar el tipo de combustible")]
        public int Combustible { get; set; }

        [DisplayName("Año")]
        [Required(ErrorMessage = "Debe de ingresar el año del vehiculo")]
        [Range(1887, int.MaxValue, ErrorMessage = "El año debe ser mayor a 1886.")]
        public int Anio { get; set; }

        [MaxLength(50, ErrorMessage = "Capacidad excedida")]
        [Required(ErrorMessage = "Debe de ingresar la capacidad del vehiculo")]
        public string Capacidad { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "Capacidad excedida")]
        [Required(ErrorMessage = "Debe de ingresar el cilindraje del vehiculo")]
        public string Cilindraje { get; set; } = null!;

        [Required(ErrorMessage = "Debe de ingresar el precio del vehiculo")]
        public decimal Precio { get; set; }

        [DisplayName("Precio Moneda")]
        [MaxLength(10, ErrorMessage = "Capacidad excedida")]
        [Required(ErrorMessage = "Debe de ingresar el precio")]
        public string MonedaPrecio { get; set; } = null!;

        [DisplayName("Plazo de los pagos")]
        [Required(ErrorMessage = "Debe de ingresar el plazo de los pagos")]
        public int PlazoOpcionAnios { get; set; }

        [DisplayName("Fecha de inicio")]
        [Required(ErrorMessage = "Debe de ingresar la fecha de inicio")]
        public string FechaInicio { get; set; }

        [DisplayName("Pago inicial")]
        [Required(ErrorMessage = "Debe de ingresar el monto del pago inicial")]
        public decimal MontoSenal { get; set; }

        [DisplayName("Monto a entregar al comprador")]
        [MaxLength(10, ErrorMessage = "Capacidad excedida")]
        [Required(ErrorMessage = "Debe de ingresar el monto a entregar al comprador")]
        public string MonedaSenal { get; set; } = null!;

        [DisplayName("Monto a devolver")]
        [Required(ErrorMessage = "Debe de ingresar el monto a devolver")]
        public decimal MontoADevolver { get; set; }

        [DisplayName("Monto a Perder")]
        [Required(ErrorMessage = "Debe de ingresar el monto a perder")]
        public decimal MontoAPerder { get; set; }

        [DisplayName("Monto perdido")]
        [Required(ErrorMessage = "Debe de ingresar el monto perdido")]
        [MaxLength(10, ErrorMessage = "Capacidad excedida")]
        public string MonedaMontoPerdido { get; set; } = null!;

        [DisplayName("Gaston de traspaso pagado por")]
        [MaxLength(150, ErrorMessage = "Capacidad excedida")]
        [Required(ErrorMessage = "Debe de ingresar el encargado de pagar el traspaso")]
        public string GastosTraspasoPagadosPor { get; set; } = null!;

        [DisplayName("Lugar de la Firma")]
        [Required(ErrorMessage = "Debe de seleccionar el lugar de la firma")]
        public int LugarFirma { get; set; }

        [DisplayName("Hora de la Firma")]
        public string? HoraFirma { get; set; }

        [DisplayName("Fecha de la Firma")]
        public string? FechaFirma { get; set; }

        [DisplayName("Cedula del Abogado en navegacion")]
        public virtual TGeAbogado? CedulaAbogadoNavigation { get; set; } = null!;

        [DisplayName("Cedula del Comprador en navegacion")]
        public virtual TGePersona? CedulaCompradorNavigation { get; set; } = null!;

        [DisplayName("Cedula del Propietario en navegacion")]
        public virtual TGePersona? CedulaPropietarioNavigation { get; set; } = null!;

        [DisplayName("Combustible en navegacion")]
        public virtual TDocsCombustible? CombustibleNavigation { get; set; } = null!;

        [DisplayName("Lugar de Firma en navegacion")]
        public virtual TCrDistrito? LugarFirmaNavigation { get; set; } = null!;

        [DisplayName("Marca del Motor en navegacion")]
        public virtual TDocsMarcaVehiculo? MarcaMotorNavigation { get; set; } = null!;

        [DisplayName("Marca del vehiculo en navegacion")]
        public virtual TDocsMarcaVehiculo? MarcaVehiculoNavigation { get; set; } = null!;

        [DisplayName("Tipo del Vehiculo en navegacion")]
        public virtual TDocsTipoVehiculo? TipoVehiculoNavigation { get; set; } = null!;
    }
}