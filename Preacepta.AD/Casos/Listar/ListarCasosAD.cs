using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD.Casos.Listar
{
    public class ListarCasosAD : IListarCasosAD
    {
        private readonly Contexto _contexto;

        public ListarCasosAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<CasoDTO>> listar()
        {
            try
            {
                /*return await _contexto.TCasos.Select(lista => new CasoDTO
                {
                    IdCaso = lista.IdCaso,
                    Fecha = lista.Fecha.ToString("dd-MM-yyyy"),
                    Nombre = lista.Nombre,
                    IdTipoCaso = lista.IdTipoCaso,
                    Descripcion = lista.Descripcion,
                    IdAbogado = lista.IdAbogado,
                    IdCliente = lista.IdCliente,
                    Activo = lista.Activo,
                    IdAbogadoNavigation = lista.IdAbogadoNavigation,
                    IdClienteNavigation = lista.IdClienteNavigation,
                    IdTipoCasoNavigation = lista.IdTipoCasoNavigation,

                }).ToListAsync();*/
                return await _contexto.TCasos
                    .Include(c => c.IdAbogadoNavigation)
                    .ThenInclude(a => a.CedulaNavigation)
                    .ThenInclude(a => a.Direccion1Navigation)
                    .Include(c => c.IdClienteNavigation)
                    .ThenInclude(a => a.Direccion1Navigation)
                    .Include(c => c.IdTipoCasoNavigation)
                    .Select(lista => new CasoDTO
                    {
                        IdCaso = lista.IdCaso,
                        Fecha = lista.Fecha.ToString("dd-MM-yyyy"),
                        Nombre = lista.Nombre,
                        IdTipoCaso = lista.IdTipoCaso,
                        Descripcion = lista.Descripcion,
                        IdAbogado = lista.IdAbogado,
                        IdCliente = lista.IdCliente,
                        Activo = lista.Activo,
                        IdAbogadoNavigation = lista.IdAbogadoNavigation,
                        IdClienteNavigation = lista.IdClienteNavigation,
                        IdTipoCasoNavigation = lista.IdTipoCasoNavigation,
                    })
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos, clase ListarCasosAD {ex.Message}");
                return new List<CasoDTO>();
            }

        }


        public async Task<List<CasoDTO>> listarXabogado(int cedula)
        {
            try
            {
                /*return await _contexto.TCasos.Select(lista => new CasoDTO
                {
                    IdCaso = lista.IdCaso,
                    Fecha = lista.Fecha.ToString("dd-MM-yyyy"),
                    Nombre = lista.Nombre,
                    IdTipoCaso = lista.IdTipoCaso,
                    Descripcion = lista.Descripcion,
                    IdAbogado = lista.IdAbogado,
                    IdCliente = lista.IdCliente,
                    Activo = lista.Activo,
                    IdAbogadoNavigation = lista.IdAbogadoNavigation,
                    IdClienteNavigation = lista.IdClienteNavigation,
                    IdTipoCasoNavigation = lista.IdTipoCasoNavigation,

                }).ToListAsync();*/
                return await _contexto.TCasos
                    .Include(c => c.IdAbogadoNavigation)
                    .ThenInclude(a => a.CedulaNavigation)
                    .ThenInclude(a => a.Direccion1Navigation)
                    .Include(c => c.IdClienteNavigation)
                    .ThenInclude(a => a.Direccion1Navigation)
                    .Include(c => c.IdTipoCasoNavigation)
                    .Select(lista => new CasoDTO
                    {
                        IdCaso = lista.IdCaso,
                        Fecha = lista.Fecha.ToString("dd-MM-yyyy"),
                        Nombre = lista.Nombre,
                        IdTipoCaso = lista.IdTipoCaso,
                        Descripcion = lista.Descripcion,
                        IdAbogado = lista.IdAbogado,
                        IdCliente = lista.IdCliente,
                        Activo = lista.Activo,
                        IdAbogadoNavigation = lista.IdAbogadoNavigation,
                        IdClienteNavigation = lista.IdClienteNavigation,
                        IdTipoCasoNavigation = lista.IdTipoCasoNavigation,
                    })
                    .Where(a => a.IdAbogado == cedula)
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos, clase ListarCasosAD {ex.Message}");
                return new List<CasoDTO>();
            }

        }

    }
}
