using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.HistorialDocumentos.Listar
{
    public class ListarHistorialAD : IListarHistorialAD
    {
        private readonly Contexto _contexto;

        public ListarHistorialAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        private IQueryable<THistorialDocumento> BaseQuery()
        {
            return _contexto.THistorialDocumentos
                .AsNoTracking()
                .Include(h => h.AbogadoNavigation)
                    .ThenInclude(a => a.CedulaNavigation)
                    .ThenInclude(p => p.Direccion1Navigation)
                .Include(h => h.ClienteNavigation)
                    .ThenInclude(p => p.Direccion1Navigation);
        }

        private static HistorialDocumentoDTO Map(THistorialDocumento h) => new()
        {
            Id = h.Id,
            Fecha = h.Fecha.ToString("dd-MM-yyyy"),
            TipoDocumento = h.TipoDocumento,
            IdDocumento = h.IdDocumento,
            Titulo = h.Titulo,
            Abogado = h.Abogado,
            Cliente = h.Cliente,
            AbogadoNavigation = h.AbogadoNavigation,
            ClienteNavigation = h.ClienteNavigation
        };

        // ----- métodos públicos -----

        public async Task<List<HistorialDocumentoDTO>> listar()
        {
            try
            {
                var datos = await BaseQuery()
                    .Include(c => c.ClienteNavigation)
                    .OrderByDescending(h => h.Fecha)
                    .ToListAsync();

                return datos.Select(Map).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar historial: {ex.Message}");
                return new List<HistorialDocumentoDTO>();
            }
        }

        public async Task<List<HistorialDocumentoDTO>> listarXabogado(int cedula)
        {
            try
            {
                var datos = await BaseQuery()
                    .Where(h => h.Abogado == cedula)
                    .OrderByDescending(h => h.Fecha)
                    .ToListAsync();

                return datos.Select(Map).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar historial por abogado: {ex.Message}");
                return new List<HistorialDocumentoDTO>();
            }
        }

        public async Task<List<HistorialDocumentoDTO>> listarXcliente(int cedula)
        {
            try
            {
                var datos = await BaseQuery()
                    .Where(h => h.Cliente == cedula)
                    .OrderByDescending(h => h.Fecha)
                    .ToListAsync();

                return datos.Select(Map).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar historial por cliente: {ex.Message}");
                return new List<HistorialDocumentoDTO>();
            }
        }

        public async Task<HistorialDocumentoDTO?> listarXultimaFecha(int cedulaAbogado)
        {
            try
            {
                var item = await BaseQuery()
                    .Where(h => h.Abogado == cedulaAbogado)
                    .OrderByDescending(h => h.Fecha)
                    .FirstOrDefaultAsync();

                return item is null ? null : Map(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener último historial por abogado: {ex.Message}");
                return null;
            }
        }

        public async Task<List<HistorialDocumentoDTO>> listarXclienteLos3Docs(int cedulaCliente)
        {
            try
            {
                var datos = await BaseQuery()
                    .Where(h => h.Cliente == cedulaCliente)
                    .OrderByDescending(h => h.Fecha)
                    .Take(3)
                    .ToListAsync();

                return datos.Select(Map).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en top 3 por cliente: {ex.Message}");
                return new List<HistorialDocumentoDTO>();
            }
        }

        public async Task<List<HistorialDocumentoDTO>> listarXabogadoLos3Docs(int cedulaAbogado)
        {
            try
            {
                var datos = await BaseQuery()
                    .Where(h => h.Abogado == cedulaAbogado)
                    .OrderByDescending(h => h.Fecha)
                    .Take(3)
                    .ToListAsync();

                return datos.Select(Map).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en top 3 por abogado: {ex.Message}");
                return new List<HistorialDocumentoDTO>();
            }
        }
    }
}
