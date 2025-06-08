using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsTipoVehiculo.Buscar
{
    public class BuscarTipoVehiculoAD : IBuscarTipoVehiculoAD
    {
        private readonly Contexto _contexto;

        public BuscarTipoVehiculoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TDocsTipoVehiculo?> buscar(int id)
        {
            try
            {
                var lista = await _contexto.TDocsTipoVehiculos.FindAsync(id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarTipoVehiculoAD, no se encontro id: {ex.Message}");
                return null;
            }
        }

    }
}
