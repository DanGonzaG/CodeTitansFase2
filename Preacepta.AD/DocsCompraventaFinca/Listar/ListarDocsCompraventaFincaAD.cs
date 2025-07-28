using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsCompraventaFinca.Listar
{
    public class ListarDocsCompraventaFincaAD : IListarDocsCompraventaFincaAD
    {
        private readonly Contexto _contexto;

        public ListarDocsCompraventaFincaAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<DocsCompraventaFincaDTO>> listar()
        {
            try
            {
                return await _contexto.TDocsCompraventaFincas.Select(lista => new DocsCompraventaFincaDTO
                {
                    AreaFincaM2 = lista.AreaFincaM2,
                    CantonFinca = lista.CantonFinca,
                    CedulaAbogado = lista.CedulaAbogado,
                    CedulaAbogadoNavigation = lista.CedulaAbogadoNavigation,
                    CedulaComprador = lista.CedulaComprador,
                    CedulaCompradorNavigation = lista.CedulaCompradorNavigation,
                    CedulaVendedor = lista.CedulaVendedor,
                    CedulaVendedorNavigation = lista.CedulaVendedorNavigation,
                    ColindaEste = lista.ColindaEste,
                    ColindaNorte = lista.ColindaNorte,
                    ColindaOeste = lista.ColindaOeste,
                    ColindaSur = lista.ColindaSur,
                    DistritoFinca = lista.DistritoFinca,
                    DistritoFincaNavigation = lista.DistritoFincaNavigation,
                    FechaFirma = lista.FechaFirma,
                    FormaPago = lista.FormaPago,
                    HoraFirma = lista.HoraFirma,
                    IdDocumento = lista.IdDocumento,
                    LugarFirma = lista.LugarFirma,
                    LugarFirmaNavigation = lista.LugarFirmaNavigation,
                    MatriculaFinca = lista.MatriculaFinca,
                    MedioPago = lista.MedioPago,
                    MontoVenta = lista.MontoVenta,
                    NaturalezaFinca = lista.NaturalezaFinca,
                    NumeroEscritura = lista.NumeroEscritura,
                    OrigenFondos = lista.OrigenFondos,
                    PartidoFinca = lista.PartidoFinca,
                    PlanoCatastrado = lista.PlanoCatastrado,
                    ProvinciaFinca = lista.ProvinciaFinca
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<DocsCompraventaFincaDTO>();
            }

        }

        public async Task<List<DocsCompraventaFincaDTO>> ListarTresUltimosDocs(int cedula)
        {
            try
            {
                return await _contexto.TDocsCompraventaFincas
                    .Include(cedula => cedula.CedulaCompradorNavigation)
                    .Where(id => id.CedulaAbogado == cedula )
                    .OrderByDescending (fecha => fecha.FechaFirma)
                    .Take(3)
                    .Select(lista => new DocsCompraventaFincaDTO
                {
                    
                    FechaFirma = lista.FechaFirma,                    
                    IdDocumento = lista.IdDocumento,
                    
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<DocsCompraventaFincaDTO>();
            }

        }

        public async Task<List<DocsCompraventaFincaDTO>> ListarTresUltimosDocsXCliente(int cedula)
        {
            try
            {
                return await _contexto.TDocsCompraventaFincas
                    .Include(cedula => cedula.CedulaAbogadoNavigation)
                    .ThenInclude(cedula2 => cedula2.CedulaNavigation)
                    .Where(id => id.CedulaComprador == cedula)
                    .OrderByDescending(fecha => fecha.FechaFirma)
                    .Take(3)
                    .Select(lista => new DocsCompraventaFincaDTO
                    {

                        FechaFirma = lista.FechaFirma,
                        IdDocumento = lista.IdDocumento,

                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<DocsCompraventaFincaDTO>();
            }

        }
    }
}
