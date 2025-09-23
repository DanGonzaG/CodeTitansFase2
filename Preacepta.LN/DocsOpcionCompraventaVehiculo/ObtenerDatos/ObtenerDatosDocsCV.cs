using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Globalization;

namespace Preacepta.LN.DocsOpcionCompraventaVehiculo.ObtenerDatos
{
    public class ObtenerDatosDocsCV : IObtenerDatosDocsCV
    {
        public DocsOpcionCompraventaVehiculoDTO ObtenerDeDB(TDocsOpcionCompraventaVehiculo e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            return new DocsOpcionCompraventaVehiculoDTO
            {
                IdDocumento = e.IdDocumento,
                NumeroEscritura = e.NumeroEscritura ?? string.Empty,

                CedulaAbogado = e.CedulaAbogado,
                CedulaPropietario = e.CedulaPropietario,
                CedulaComprador = e.CedulaComprador,

                PlacaVehiculo = e.PlacaVehiculo ?? string.Empty,
                MarcaVehiculo = e.MarcaVehiculo,
                TipoVehiculo = e.TipoVehiculo,
                ModeloVehiculo = e.ModeloVehiculo ?? string.Empty,
                Carroceria = e.Carroceria ?? string.Empty,
                Categoria = e.Categoria ?? string.Empty,
                Chasis = e.Chasis ?? string.Empty,
                Serie = e.Serie ?? string.Empty,
                Vin = e.Vin ?? string.Empty,
                MarcaMotor = e.MarcaMotor,
                NumeroMotor = e.NumeroMotor ?? string.Empty,
                Color = e.Color ?? string.Empty,
                Combustible = e.Combustible,
                Anio = e.Anio,
                Capacidad = e.Capacidad,
                Cilindraje = e.Cilindraje,

                Precio = e.Precio,
                MonedaPrecio = e.MonedaPrecio ?? string.Empty,
                PlazoOpcionAnios = e.PlazoOpcionAnios,

                FechaInicio = e.FechaInicio.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),

                MontoSenal = e.MontoSenal,
                MonedaSenal = e.MonedaSenal ?? string.Empty,
                MontoADevolver = e.MontoADevolver,
                MontoAPerder = e.MontoAPerder,
                MonedaMontoPerdido = e.MonedaMontoPerdido ?? string.Empty,
                GastosTraspasoPagadosPor = e.GastosTraspasoPagadosPor ?? string.Empty,

                LugarFirma = e.LugarFirma,

                HoraFirma = e.HoraFirma.ToString("HH:mm", CultureInfo.InvariantCulture),
                FechaFirma = e.FechaFirma.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),

                CedulaAbogadoNavigation = null,
                CedulaCompradorNavigation = null,
                CedulaPropietarioNavigation = null,
                CombustibleNavigation = null,
                LugarFirmaNavigation = null,
                MarcaMotorNavigation = null,
                MarcaVehiculoNavigation = null,
                TipoVehiculoNavigation = null
            };
        }

        public TDocsOpcionCompraventaVehiculo ObtenerDeFront(DocsOpcionCompraventaVehiculoDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            // Acepta "yyyy-MM-dd" (input date), "dd/MM/yyyy" y "dd-MM-yyyy"
            static DateOnly ParseDateOnly(string? s)
            {
                if (string.IsNullOrWhiteSpace(s))
                    return DateOnly.FromDateTime(DateTime.Today);

                var formats = new[] { "yyyy-MM-dd", "dd/MM/yyyy", "dd-MM-yyyy", "MM/dd/yyyy" };
                if (DateTime.TryParseExact(s, formats, CultureInfo.InvariantCulture,
                                           DateTimeStyles.None, out var d))
                    return DateOnly.FromDateTime(d);

                if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out var d2))
                    return DateOnly.FromDateTime(d2);

                return DateOnly.FromDateTime(DateTime.Today);
            }

            return new TDocsOpcionCompraventaVehiculo
            {
                IdDocumento = dto.IdDocumento,
                NumeroEscritura = dto.NumeroEscritura,
                CedulaAbogado = dto.CedulaAbogado,
                CedulaPropietario = dto.CedulaPropietario,
                CedulaComprador = dto.CedulaComprador,

                PlacaVehiculo = dto.PlacaVehiculo,
                MarcaVehiculo = dto.MarcaVehiculo,
                TipoVehiculo = dto.TipoVehiculo,
                ModeloVehiculo = dto.ModeloVehiculo,
                Carroceria = dto.Carroceria,
                Categoria = dto.Categoria,
                Chasis = dto.Chasis,
                Serie = dto.Serie,
                Vin = dto.Vin,
                MarcaMotor = dto.MarcaMotor,
                NumeroMotor = dto.NumeroMotor,
                Color = dto.Color,
                Combustible = dto.Combustible,
                Anio = dto.Anio,
                Capacidad = dto.Capacidad,
                Cilindraje = dto.Cilindraje,

                Precio = dto.Precio,
                MonedaPrecio = dto.MonedaPrecio,
                PlazoOpcionAnios = dto.PlazoOpcionAnios,

                FechaInicio = ParseDateOnly(dto.FechaInicio),

                MontoSenal = dto.MontoSenal,
                MonedaSenal = dto.MonedaSenal,
                MontoADevolver = dto.MontoADevolver,
                MontoAPerder = dto.MontoAPerder,
                MonedaMontoPerdido = dto.MonedaMontoPerdido,
                GastosTraspasoPagadosPor = dto.GastosTraspasoPagadosPor,

                LugarFirma = dto.LugarFirma,

                FechaFirma = DateOnly.FromDateTime(DateTime.Now),
                HoraFirma = TimeOnly.FromDateTime(DateTime.Now),

                CedulaAbogadoNavigation = null,
                CedulaCompradorNavigation = null,
                CedulaPropietarioNavigation = null,
                CombustibleNavigation = null,
                LugarFirmaNavigation = null,
                MarcaMotorNavigation = null,
                MarcaVehiculoNavigation = null,
                TipoVehiculoNavigation = null
            };
        }
    }
}
