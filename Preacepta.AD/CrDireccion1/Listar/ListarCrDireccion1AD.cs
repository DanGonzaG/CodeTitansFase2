using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD.CrDireccion1.Listar
{
    public class ListarCrDireccion1AD : IListarCrDireccion1AD
    {
        private readonly Contexto _contexto;

        public ListarCrDireccion1AD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<CrProvinciaDTO>> listarProvincias()
        {
            try
            {
                return await _contexto.TCrProvincias
                    .Select(lista => new CrProvinciaDTO
                    {
                        IdProvincia = lista.IdProvincia,
                        NombreProvincia = lista.NombreProvincia,
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos de ListarCrDireccion1AD-listarProvincias {ex.Message}");
                return new List<CrProvinciaDTO>();
            }
        }

        public async Task<List<CrCantonDTO>> listarCantones()
        {
            try
            {
                return await _contexto.TCrCantones
                    .Include(a => a.IdProvinciaNavigation)
                    .Select(lista => new CrCantonDTO
                    {
                        IdCanton = lista.IdCanton,
                        IdProvincia = lista.IdCanton,
                        NombreCanton = lista.NombreCanton,
                        IdProvinciaNavigation = lista.IdProvinciaNavigation,
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos de ListarCrDireccion1AD-listarCantones {ex.Message}");
                return new List<CrCantonDTO>();
            }
        }

        public async Task<List<CrCantonDTO>> listarCantonesXprovincia(int id)
        {
            try
            {
                return await _contexto.TCrCantones

                    .Include(a => a.IdProvinciaNavigation)
                    .Where(a => a.IdProvincia == id)
                    .Select(lista => new CrCantonDTO                    
                    {
                        IdCanton = lista.IdCanton,
                        IdProvincia = lista.IdCanton,
                        NombreCanton = lista.NombreCanton,
                        IdProvinciaNavigation = lista.IdProvinciaNavigation,
                    })
                    
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos de ListarCrDireccion1AD-listarCantones {ex.Message}");
                return new List<CrCantonDTO>();
            }
        }

        public async Task<List<CrDistritoDTO>> listarDistritos()
        {
            try
            {
                return await _contexto.TCrDistritos
                    .Include(a => a.IdCatonNavigation)
                    .Select(lista => new CrDistritoDTO
                    {
                        IdDistrito = lista.IdDistrito,
                        IdCaton = lista.IdCaton,
                        IdCatonNavigation = lista.IdCatonNavigation,
                        NombreDistrito = lista.NombreDistrito,
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos de ListarCrDireccion1AD-listarDistritos {ex.Message}");
                return new List<CrDistritoDTO>();
            }
        }

        public async Task<List<CrDistritoDTO>> listarDistritosXcantones(int id)
        {
            try
            {
                return await _contexto.TCrDistritos
                    .Include(a => a.IdCatonNavigation)
                    .Where(a => a.IdCaton == id)
                    .Select(lista => new CrDistritoDTO
                    {
                        IdDistrito = lista.IdDistrito,
                        IdCaton = lista.IdCaton,
                        IdCatonNavigation = lista.IdCatonNavigation,
                        NombreDistrito = lista.NombreDistrito,
                    })                    
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos de ListarCrDireccion1AD-listarDistritos {ex.Message}");
                return new List<CrDistritoDTO>();
            }
        }
    }
}
