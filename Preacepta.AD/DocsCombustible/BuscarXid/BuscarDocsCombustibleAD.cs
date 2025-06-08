using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsCombustible.BuscarXid
{
    public class BuscarDocsCombustibleAD : IBuscarDocsCombustibleAD
    {
        private readonly Contexto _contexto;

        public BuscarDocsCombustibleAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TDocsCombustible?> buscar(int id)
        {
            try
            {
                var lista = await _contexto.TDocsCombustibles
                .FirstOrDefaultAsync(m => m.Id == id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarDocsCombustibleAD, no se encontro id: {ex.Message}");
                return null;
            }

        }
    }
}
