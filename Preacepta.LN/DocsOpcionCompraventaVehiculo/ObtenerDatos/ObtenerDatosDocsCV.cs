using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace Preacepta.LN.DocsOpcionCompraventaVehiculo.ObtenerDatos
{
    public class ObtenerDatosDocsCV : IObtenerDatosDocsCV
    {
        public DocsOpcionCompraventaVehiculoDTO ObtenerDeDB(TDocsOpcionCompraventaVehiculo ComVen)
        {
            return new DocsOpcionCompraventaVehiculoDTO
            {
                IdDocumento = ComVen.IdDocumento,
                NumeroEscritura = ComVen.NumeroEscritura,
                CedulaAbogado = ComVen.CedulaAbogado,
                CedulaPropietario = ComVen.CedulaPropietario,
                CedulaComprador = ComVen.CedulaComprador,
                PlacaVehiculo = ComVen.PlacaVehiculo,
                MarcaVehiculo = ComVen.MarcaVehiculo,
                TipoVehiculo = ComVen.TipoVehiculo,
                ModeloVehiculo = ComVen.ModeloVehiculo,
                Carroceria = ComVen.Carroceria,
                Categoria = ComVen.Categoria,
                Chasis = ComVen.Chasis,
                Serie = ComVen.Serie,
                Vin = ComVen.Vin,
                MarcaMotor = ComVen.MarcaMotor,
                NumeroMotor = ComVen.NumeroMotor,
                Color = ComVen.Color,
                Combustible = ComVen.Combustible,
                Anio = ComVen.Anio,
                Capacidad = ComVen.Capacidad,
                Cilindraje = ComVen.Cilindraje,
                Precio = ComVen.Precio,
                MonedaPrecio = ComVen.MonedaPrecio,
                PlazoOpcionAnios = ComVen.PlazoOpcionAnios,
                FechaInicio = ComVen.FechaInicio.ToString("yyyy-MM-dd HH:mm"),
                MontoSenal = ComVen.MontoSenal,
                MonedaSenal = ComVen.MonedaSenal,
                MontoADevolver = ComVen.MontoADevolver,
                MontoAPerder = ComVen.MontoAPerder,
                MonedaMontoPerdido = ComVen.MonedaMontoPerdido,
                GastosTraspasoPagadosPor = ComVen.GastosTraspasoPagadosPor,
                LugarFirma = ComVen.LugarFirma,
                HoraFirma = ComVen.HoraFirma.ToString("HH:mm"),
                FechaFirma = ComVen.FechaFirma.ToString("yyyy-MM-dd"),
                CedulaAbogadoNavigation = ComVen.CedulaAbogadoNavigation,
                CedulaCompradorNavigation = ComVen.CedulaCompradorNavigation,
                CedulaPropietarioNavigation = ComVen.CedulaPropietarioNavigation,
                CombustibleNavigation = ComVen.CombustibleNavigation,
                LugarFirmaNavigation = ComVen.LugarFirmaNavigation,
                MarcaMotorNavigation = ComVen.MarcaMotorNavigation,
                MarcaVehiculoNavigation = ComVen.MarcaVehiculoNavigation,
                TipoVehiculoNavigation = ComVen.TipoVehiculoNavigation
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TDocsOpcionCompraventaVehiculo ObtenerDeFront(DocsOpcionCompraventaVehiculoDTO ComVenDTO)
        {
            return new TDocsOpcionCompraventaVehiculo
            {
                IdDocumento = ComVenDTO.IdDocumento,
                NumeroEscritura = ComVenDTO.NumeroEscritura,
                CedulaAbogado = ComVenDTO.CedulaAbogado,
                CedulaPropietario = ComVenDTO.CedulaPropietario,
                CedulaComprador = ComVenDTO.CedulaComprador,
                PlacaVehiculo = ComVenDTO.PlacaVehiculo,
                MarcaVehiculo = ComVenDTO.MarcaVehiculo,
                TipoVehiculo = ComVenDTO.TipoVehiculo,
                ModeloVehiculo = ComVenDTO.ModeloVehiculo,
                Carroceria = ComVenDTO.Carroceria,
                Categoria = ComVenDTO.Categoria,
                Chasis = ComVenDTO.Chasis,
                Serie = ComVenDTO.Serie,
                Vin = ComVenDTO.Vin,
                MarcaMotor = ComVenDTO.MarcaMotor,
                NumeroMotor = ComVenDTO.NumeroMotor,
                Color = ComVenDTO.Color,
                Combustible = ComVenDTO.Combustible,
                Anio = ComVenDTO.Anio,
                Capacidad = ComVenDTO.Capacidad,
                Cilindraje = ComVenDTO.Cilindraje,
                Precio = ComVenDTO.Precio,
                MonedaPrecio = ComVenDTO.MonedaPrecio,
                PlazoOpcionAnios = ComVenDTO.PlazoOpcionAnios,
                FechaInicio = DateOnly.FromDateTime(DateTime.Now),
                MontoSenal = ComVenDTO.MontoSenal,
                MonedaSenal = ComVenDTO.MonedaSenal,
                MontoADevolver = ComVenDTO.MontoADevolver,
                MontoAPerder = ComVenDTO.MontoAPerder,
                MonedaMontoPerdido = ComVenDTO.MonedaMontoPerdido,
                GastosTraspasoPagadosPor = ComVenDTO.GastosTraspasoPagadosPor,
                LugarFirma = ComVenDTO.LugarFirma,
                HoraFirma = TimeOnly.ParseExact(ComVenDTO.HoraFirma, "HH:mm", CultureInfo.InvariantCulture),
                FechaFirma = DateOnly.ParseExact(ComVenDTO.FechaFirma, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                CedulaAbogadoNavigation = ComVenDTO.CedulaAbogadoNavigation,
                CedulaCompradorNavigation = ComVenDTO.CedulaCompradorNavigation,
                CedulaPropietarioNavigation = ComVenDTO.CedulaPropietarioNavigation,
                CombustibleNavigation = ComVenDTO.CombustibleNavigation,
                LugarFirmaNavigation = ComVenDTO.LugarFirmaNavigation,
                MarcaMotorNavigation = ComVenDTO.MarcaMotorNavigation,
                MarcaVehiculoNavigation = ComVenDTO.MarcaVehiculoNavigation,
                TipoVehiculoNavigation = ComVenDTO.TipoVehiculoNavigation
            };
        }
    }
}
