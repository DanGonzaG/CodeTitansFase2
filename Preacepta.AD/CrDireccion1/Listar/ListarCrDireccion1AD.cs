using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return await _contexto.TCrProvincias.Select(lista => new CrProvinciaDTO
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
                return await _contexto.TCrCantones.Select(lista => new CrCantonDTO
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

        public async Task<List<CrDistritoDTO>> listarDistritos()
        {
            try
            {
                return await _contexto.TCrDistritos.Select(lista => new CrDistritoDTO
                {
                    IdDistrito= lista.IdDistrito,
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
    }
}
