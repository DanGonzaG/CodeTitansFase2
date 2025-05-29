using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Casos.BuscarXid
{
    public class BuscarCasosAD : IBuscarCasosAD
    {
        private readonly Contexto _contexto;

        public BuscarCasosAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TCaso?> buscar(int id)
        {
            try
            {
                var lista = await _contexto.TCasos
                .Include(t => t.IdAbogadoNavigation)
                .Include(t => t.IdClienteNavigation)
                .Include(t => t.IdTipoCasoNavigation)
                .FirstOrDefaultAsync(m => m.IdCaso == id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCasosAD, no se encontro id: {ex.Message}");
                return null;
            }

        }
    }
}
