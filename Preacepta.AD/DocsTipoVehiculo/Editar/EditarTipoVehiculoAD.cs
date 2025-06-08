using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsTipoVehiculo.Editar
{
    public class EditarTipoVehiculoAD : IEditarTipoVehiculoAD
    {
        private readonly Contexto _contexto;

        public EditarTipoVehiculoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> editar(TDocsTipoVehiculo tipovehiculo)
        {
            if (tipovehiculo == null)
            {
                return 0;
            }

            try
            {
                _contexto.TDocsTipoVehiculos.Update(tipovehiculo);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarTipoVehiculoAD : {ex.Message}");
                return -1;
            }
        }

    }
}
