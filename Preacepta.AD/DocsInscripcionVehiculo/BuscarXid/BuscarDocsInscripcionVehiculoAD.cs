using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsInscripcionVehiculo.BuscarXid
{
    public class BuscarDocsInscripcionVehiculoAD : IBuscarDocsInscripcionVehiculoAD
    {
        private readonly Contexto _contexto;

        public BuscarDocsInscripcionVehiculoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TDocsInscripcionVehiculo?> buscar(int id)
        {
            try
            {
                var lista = await _contexto.TDocsInscripcionVehiculos
                .Include(t => t.CedulaAbogadoNavigation)
                .Include(t => t.CedulaClienteNavigation)
                .Include(t => t.EstiloVehiculoNavigation)
                .Include(t => t.LugarFirmaNavigation)
                .Include(t => t.MarcaVehiculoNavigation)
                .FirstOrDefaultAsync(m => m.IdDocumento == id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarDocsInscripcionVehiculoAD, no se encontro id: {ex.Message}");
                return null;
            }

        }
    }
}
