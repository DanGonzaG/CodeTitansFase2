using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CasosEvidencia.BuscarXid
{
    public class BuscarCasosEvidenciaAD : IBuscarCasosEvidenciaAD
    {
        private readonly Contexto _contexto;

        public BuscarCasosEvidenciaAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TCasosEvidencia?> buscar(int id)
        {
            try
            {
                var lista = await _contexto.TCasosEvidencias
                .Include(t => t.IdCaso1)
                .Include(t => t.IdCasoNavigation)
                .FirstOrDefaultAsync(m => m.IdEvidencia == id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCasosEvidenciaAD, no se encontro id: {ex.Message}");
                return null;
            }

        }
    }
}
