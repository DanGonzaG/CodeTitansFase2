using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsCompraventaFinca.BuscarXid
{
    public class BuscarDocsCompraventaFincaAD : IBuscarDocsCompraventaFincaAD
    {
        private readonly Contexto _contexto;

        public BuscarDocsCompraventaFincaAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TDocsCompraventaFinca?> buscar(int id)
        {
            try
            {
                var lista = await _contexto.TDocsCompraventaFincas
                .Include(t => t.CedulaAbogadoNavigation)
                .Include(t => t.CedulaCompradorNavigation)
                .Include(t => t.CedulaVendedorNavigation)
                .Include(t => t.DistritoFincaNavigation)
                .Include(t => t.LugarFirmaNavigation)
                .FirstOrDefaultAsync(m => m.IdDocumento == id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarDocsCompraventaFincaAD, no se encontro id: {ex.Message}");
                return null;
            }

        }
    }
}
