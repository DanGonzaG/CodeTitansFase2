using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsMarcaVehiculo.BuscarXid
{
    public class BuscarDocsMarcaVehiculoAD : IBuscarDocsMarcaVehiculoAD
    {
        private readonly Contexto _contexto;

        public BuscarDocsMarcaVehiculoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TDocsMarcaVehiculo?> buscar(int id)
        {
            try
            {
                var lista = await _contexto.TDocsMarcaVehiculos
                .FirstOrDefaultAsync(m => m.Id == id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarDocsMarcaVehiculoAD, no se encontro id: {ex.Message}");
                return null;
            }

        }
    }
}
