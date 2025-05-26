using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeAbogado.BuscarXid
{
    public class BuscarAbogadoAD : IBuscarAbogadoAD
    {
        private readonly Contexto _contexto;

        public BuscarAbogadoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TGeAbogado?> buscar(int id)
        {
            try
            {
                var lista = await _contexto.TGeAbogados
                .Include(t => t.CJuridicaNavigation)
                .Include(t => t.CedulaNavigation)
                .Include(t => t.IdTipoAbogadoNavigation)
                .FirstOrDefaultAsync(m => m.Cedula == id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarAbogadoAD, no se encontro id: {ex.Message}");
                return null;
            }

        }
    }
}
