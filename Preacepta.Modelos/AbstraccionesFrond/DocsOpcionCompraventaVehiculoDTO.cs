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
        public string NumeroEscritura { get; set; } = null!;

        [DisplayName("Cedula del abogado")]
        public int CedulaAbogado { get; set; }

        [DisplayName("Cedula del propietario")]
        public int CedulaPropietario { get; set; }

        [DisplayName("Cedula del comprador")]
        public int CedulaComprador { get; set; }

        [DisplayName("Placa del vehiculo")]
        [MaxLength(20, ErrorMessage = "Capacidad de la placa excedida")]
        public string PlacaVehiculo { get; set; } = null!;

        [DisplayName("Marca del vehiculo")]
        public int MarcaVehiculo { get; set; }

        [DisplayName("Tipo de vehiculo")]
        public int TipoVehiculo { get; set; }

        [DisplayName("Modelo del vehiculo")]
        [MaxLength(100, ErrorMessage = "Capacidad excedida")]
        public string ModeloVehiculo { get; set; } = null!;

        [MaxLength(100, ErrorMessage = "Capacidad excedida")]
        public string Carroceria { get; set; } = null!;

        [MaxLength(100, ErrorMessage = "Capacidad excedida")]
        public string Categoria { get; set; } = null!;

        [MaxLength(100, ErrorMessage = "Capacidad excedida")]
        public string Chasis { get; set; } = null!;

        [MaxLength(100, ErrorMessage = "Capacidad excedida")]
        public string Serie { get; set; } = null!;

        [MaxLength(100, ErrorMessage = "Capacidad excedida")]
        public string Vin { get; set; } = null!;

        [DisplayName("Marca del motor")]
        public int MarcaMotor { get; set; }

        [DisplayName("Numero del motor")]
        [MaxLength(100, ErrorMessage = "Capacidad excedida")]
        public string NumeroMotor { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "Capacidad excedida")]
        public string Color { get; set; } = null!;

        public int Combustible { get; set; }

        [DisplayName("Año")]
        public int Anio { get; set; }

        [MaxLength(50, ErrorMessage = "Capacidad excedida")]
        public string Capacidad { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "Capacidad excedida")]
        public string Cilindraje { get; set; } = null!;

        public decimal Precio { get; set; }

        [DisplayName("Precio Moneda")]
        [MaxLength(10, ErrorMessage = "Capacidad de moneda_precio excedida")]
        public string MonedaPrecio { get; set; } = null!;

        [DisplayName("Plazo o Opciones Año")]
        public int PlazoOpcionAnios { get; set; }

        [DisplayName("Fecha de inicio")]
        public string FechaInicio { get; set; }

        [DisplayName("Monto Senal")]
        public decimal MontoSenal { get; set; }

        [DisplayName("Moneda Senal")]
        [MaxLength(10, ErrorMessage = "Capacidad excedida")]
        public string MonedaSenal { get; set; } = null!;

        [DisplayName("Monto a devolver")]
        public decimal MontoADevolver { get; set; }

        [DisplayName("Monto a Perder")]
        public decimal MontoAPerder { get; set; }

        [DisplayName("Moneda de monto perdido")]
        [MaxLength(10, ErrorMessage = "Capacidad excedida")]
        public string MonedaMontoPerdido { get; set; } = null!;

        [DisplayName("Gaston de traspaso pagado por")]
        [MaxLength(150, ErrorMessage = "Capacidad excedida")]
        public string GastosTraspasoPagadosPor { get; set; } = null!;

        [DisplayName("Lugar de la Firma")]
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
