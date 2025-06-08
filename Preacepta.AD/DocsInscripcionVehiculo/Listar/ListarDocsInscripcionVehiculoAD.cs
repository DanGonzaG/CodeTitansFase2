using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsInscripcionVehiculo.Listar
{
    public class ListarDocsInscripcionVehiculoAD : IListarDocsInscripcionVehiculoAD
    {
        private readonly Contexto _contexto;

        public ListarDocsInscripcionVehiculoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<DocsInscripcionVehiculoDTO>> listar()
        {
            try
            {
                return await _contexto.TDocsInscripcionVehiculos.Select(lista => new DocsInscripcionVehiculoDTO
                {
                    Anio = lista.Anio,
                    Capacidad = lista.Capacidad,
                    Carroceria = lista.Carroceria,
                    Categoria = lista.Categoria,
                    CedulaAbogado = lista.CedulaAbogado,
                    CedulaAbogadoNavigation = lista.CedulaAbogadoNavigation,
                    CedulaCliente = lista.CedulaCliente,
                    CedulaClienteNavigation = lista.CedulaClienteNavigation,
                    Cilindraje = lista.Cilindraje,
                    Color = lista.Color,
                    Combustible = lista.Combustible,
                    EstiloVehiculo = lista.EstiloVehiculo,
                    EstiloVehiculoNavigation = lista.EstiloVehiculoNavigation,
                    FechaFirma = lista.FechaFirma,
                    IdDocumento = lista.IdDocumento,
                    LugarFirma = lista.LugarFirma,
                    LugarFirmaNavigation = lista.LugarFirmaNavigation,
                    MarcaMotor = lista.MarcaMotor,
                    MarcaVehiculo = lista.MarcaVehiculo,
                    MarcaVehiculoNavigation = lista.MarcaVehiculoNavigation,
                    ModeloVehiculo = lista.ModeloVehiculo,
                    NumeroMotor = lista.NumeroMotor,
                    NumeroSerieChasis = lista.NumeroSerieChasis,
                    PesoBruto = lista.PesoBruto,
                    PesoNeto = lista.PesoNeto,
                    Potencia = lista.Potencia,
                    Vin = lista.Vin
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<DocsInscripcionVehiculoDTO>();
            }

        }
    }
}
