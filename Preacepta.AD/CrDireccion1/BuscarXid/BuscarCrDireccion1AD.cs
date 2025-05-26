using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CrDireccion1.BuscarXid
{
    public class BuscarCrDireccion1AD : IBuscarCrDireccion1AD
    {
        private readonly Contexto _contexto;

        public BuscarCrDireccion1AD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TCrProvincia?> buscarProvincias(int id)
        {
            try
            {
                var lista = await _contexto.TCrProvincias.FindAsync(id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCrDireccion1AD-list-TCrProvincia, no se encontro id: {ex.Message}");
                return null;
            }
        }

        public async Task<TCrCantone?> buscarCantones(int id)
        {
            try
            {
                //var lista = await _contexto.TCrCantones.FindAsync(id);
                var lista = await _contexto.TCrCantones
                .Include(t => t.IdProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.IdCanton == id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCrDireccion1AD-list-TCrCantones, no se encontro id: {ex.Message}");
                return null;
            }
        }

        public async Task<TCrDistrito?> buscarDistritos(int id)
        {
            try
            {
                var lista = await _contexto.TCrDistritos.FindAsync(id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCrDireccion1AD-list-TCrDistrito, no se encontro id: {ex.Message}");
                return null;
            }
        }
    }
}
