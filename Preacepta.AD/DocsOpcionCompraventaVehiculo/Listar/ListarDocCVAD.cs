using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsOpcionCompraventaVehiculo.Listar
{
    public class ListarDocCVAD : IListarDocCVAD
    {
        private readonly Contexto _contexto;
        public ListarDocCVAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<DocsOpcionCompraventaVehiculoDTO>> listar2()
        {
            List<DocsOpcionCompraventaVehiculoDTO> lista = await (from DocsCV in _contexto.TDocsOpcionCompraventaVehiculos
                                                                  select new DocsOpcionCompraventaVehiculoDTO
                                                                  {
                                                                      IdDocumento = DocsCV.IdDocumento,
                                                                      NumeroEscritura = DocsCV.NumeroEscritura,
                                                                      CedulaAbogado = DocsCV.CedulaAbogado,
                                                                      CedulaPropietario = DocsCV.CedulaPropietario,
                                                                      CedulaComprador = DocsCV.CedulaComprador,
                                                                      PlacaVehiculo = DocsCV.PlacaVehiculo,
                                                                      MarcaVehiculo = DocsCV.MarcaVehiculo,
                                                                      TipoVehiculo = DocsCV.TipoVehiculo,
                                                                      ModeloVehiculo = DocsCV.ModeloVehiculo,
                                                                      Carroceria = DocsCV.Carroceria,
                                                                      Categoria = DocsCV.Categoria,
                                                                      Chasis = DocsCV.Chasis,
                                                                      Serie = DocsCV.Serie,
                                                                      Vin = DocsCV.Vin,
                                                                      MarcaMotor = DocsCV.MarcaMotor,
                                                                      NumeroMotor = DocsCV.NumeroMotor,
                                                                      Color = DocsCV.Color,
                                                                      Combustible = DocsCV.Combustible,
                                                                      Anio = DocsCV.Anio,
                                                                      Capacidad = DocsCV.Capacidad,
                                                                      Cilindraje = DocsCV.Cilindraje,
                                                                      Precio = DocsCV.Precio,
                                                                      MonedaPrecio = DocsCV.MonedaPrecio,
                                                                      PlazoOpcionAnios = DocsCV.PlazoOpcionAnios,
                                                                      FechaInicio = DocsCV.FechaInicio.ToString("yyyy-MM-dd HH:mm"),
                                                                      MontoSenal = DocsCV.MontoSenal,
                                                                      MonedaSenal = DocsCV.MonedaSenal,
                                                                      MontoADevolver = DocsCV.MontoADevolver,
                                                                      MontoAPerder = DocsCV.MontoAPerder,
                                                                      MonedaMontoPerdido = DocsCV.MonedaMontoPerdido,
                                                                      GastosTraspasoPagadosPor = DocsCV.GastosTraspasoPagadosPor,
                                                                      LugarFirma = DocsCV.LugarFirma,
                                                                      HoraFirma = DocsCV.HoraFirma.ToString("HH:mm"),
                                                                      FechaFirma = DocsCV.FechaFirma.ToString("yyyy-MM-dd"),
                                                                      CedulaAbogadoNavigation = DocsCV.CedulaAbogadoNavigation,
                                                                      CedulaCompradorNavigation = DocsCV.CedulaCompradorNavigation,
                                                                      CedulaPropietarioNavigation = DocsCV.CedulaPropietarioNavigation,
                                                                      CombustibleNavigation = DocsCV.CombustibleNavigation,
                                                                      LugarFirmaNavigation = DocsCV.LugarFirmaNavigation,
                                                                      MarcaMotorNavigation = DocsCV.MarcaMotorNavigation,
                                                                      MarcaVehiculoNavigation = DocsCV.MarcaVehiculoNavigation,
                                                                      TipoVehiculoNavigation = DocsCV.TipoVehiculoNavigation
                                                                  }).ToListAsync();
            return lista;
        }

        public async Task<List<DocsOpcionCompraventaVehiculoDTO>> Listar()
        {


            try
            {
                return await _contexto.TDocsOpcionCompraventaVehiculos.Select(DocsCV => new DocsOpcionCompraventaVehiculoDTO
                {
                    IdDocumento = DocsCV.IdDocumento,
                    NumeroEscritura = DocsCV.NumeroEscritura,
                    CedulaAbogado = DocsCV.CedulaAbogado,
                    CedulaPropietario = DocsCV.CedulaPropietario,
                    CedulaComprador = DocsCV.CedulaComprador,
                    PlacaVehiculo = DocsCV.PlacaVehiculo,
                    MarcaVehiculo = DocsCV.MarcaVehiculo,
                    TipoVehiculo = DocsCV.TipoVehiculo,
                    ModeloVehiculo = DocsCV.ModeloVehiculo,
                    Carroceria = DocsCV.Carroceria,
                    Categoria = DocsCV.Categoria,
                    Chasis = DocsCV.Chasis,
                    Serie = DocsCV.Serie,
                    Vin = DocsCV.Vin,
                    MarcaMotor = DocsCV.MarcaMotor,
                    NumeroMotor = DocsCV.NumeroMotor,
                    Color = DocsCV.Color,
                    Combustible = DocsCV.Combustible,
                    Anio = DocsCV.Anio,
                    Capacidad = DocsCV.Capacidad,
                    Cilindraje = DocsCV.Cilindraje,
                    Precio = DocsCV.Precio,
                    MonedaPrecio = DocsCV.MonedaPrecio,
                    PlazoOpcionAnios = DocsCV.PlazoOpcionAnios,
                    FechaInicio = DocsCV.FechaInicio.ToString("yyyy-MM-dd HH:mm"),
                    MontoSenal = DocsCV.MontoSenal,
                    MonedaSenal = DocsCV.MonedaSenal,
                    MontoADevolver = DocsCV.MontoADevolver,
                    MontoAPerder = DocsCV.MontoAPerder,
                    MonedaMontoPerdido = DocsCV.MonedaMontoPerdido,
                    GastosTraspasoPagadosPor = DocsCV.GastosTraspasoPagadosPor,
                    LugarFirma = DocsCV.LugarFirma,
                    HoraFirma = DocsCV.HoraFirma.ToString("HH:mm"),
                    FechaFirma = DocsCV.FechaFirma.ToString("yyyy-MM-dd"),
                    CedulaAbogadoNavigation = DocsCV.CedulaAbogadoNavigation,
                    CedulaCompradorNavigation = DocsCV.CedulaCompradorNavigation,
                    CedulaPropietarioNavigation = DocsCV.CedulaPropietarioNavigation,
                    CombustibleNavigation = DocsCV.CombustibleNavigation,
                    LugarFirmaNavigation = DocsCV.LugarFirmaNavigation,
                    MarcaMotorNavigation = DocsCV.MarcaMotorNavigation,
                    MarcaVehiculoNavigation = DocsCV.MarcaVehiculoNavigation,
                    TipoVehiculoNavigation = DocsCV.TipoVehiculoNavigation
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<DocsOpcionCompraventaVehiculoDTO>();
            }
        }
    }
}
