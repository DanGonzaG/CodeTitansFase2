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
                return await _contexto.TCasos
                    .Include(c => c.IdAbogadoNavigation)
                    .ThenInclude(a => a.CedulaNavigation)
                    .ThenInclude(a => a.Direccion1Navigation)
                    .Include(c => c.IdClienteNavigation)
                    .ThenInclude(a => a.Direccion1Navigation)
                    .Include(c => c.IdTipoCasoNavigation)
                    .Where(a => a.IdAbogado == cedula)
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

        public async Task<List<CasoDTO>> listarXcliente(int cedula)
        {
            try
            {
                return await _contexto.TCasos
                    .Include(c => c.IdAbogadoNavigation)
                    .ThenInclude(a => a.CedulaNavigation)
                    .ThenInclude(a => a.Direccion1Navigation)
                    .Include(c => c.IdClienteNavigation)
                    .ThenInclude(a => a.Direccion1Navigation)
                    .Include(c => c.IdTipoCasoNavigation)
                    .Where(a => a.IdCliente == cedula)
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


        /*este metodo se usa para mostrar los 3 ultimos casos  por CLIENTE creados en la tabla Tcasos*/
        public async Task<List<CasoDTO>> listarXclienteLos3Casos(int cedula)
        {
            try
            {
                return await _contexto.TCasos                    
                    .Where(a => a.IdCliente == cedula)
                    .OrderByDescending(fecha => fecha.Fecha)
                    .Take(3)
                    .Select(lista => new CasoDTO
                    {
                        IdCaso = lista.IdCaso,
                        Nombre = lista.Nombre, 
                        Activo = lista.Activo
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos, clase listarXclienteLos3Casos {ex.Message}");
                return new List<CasoDTO>();
            }

        }

        /*este metodo se usa para mostrar los 3 ultimos casos  por ABOGADO creados en la tabla Tcasos*/
        public async Task<List<CasoDTO>> listarXabogadoLos3Casos(int cedula)
        {
            try
            {
                return await _contexto.TCasos
                    .Where(a => a.IdAbogado == cedula && a.Activo == true)
                    .OrderByDescending(fecha => fecha.Fecha)
                    .Take(3)
                    .Select(lista => new CasoDTO
                    {
                        IdCaso = lista.IdCaso,
                        Nombre = lista.Nombre,
                        Activo = lista.Activo
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos, clase listarXabogadoLos3Casos {ex.Message}");
                return new List<CasoDTO>();
            }

        }


        /*este metodo se usa para mostrar los ultimos casos creados en la tabla Tcasos*/
        public async Task<CasoDTO> listarXultimaFecha(int cedula)
        {
            try
            {
                return await _contexto.TCasos
                    .Include(c => c.IdAbogadoNavigation)
                    .ThenInclude(a => a.CedulaNavigation)
                    .ThenInclude(a => a.Direccion1Navigation)
                    .Include(c => c.IdClienteNavigation)
                    .ThenInclude(a => a.Direccion1Navigation)
                    .Include(c => c.IdTipoCasoNavigation)
                    .Where(a => a.IdAbogado == cedula)
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
                    .FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos, clase ListarCasosAD {ex.Message}");
                return new CasoDTO();
            }

        }

    }
}
