using Preacepta.AD.DocsTipoVehiculo.Editar;
using Preacepta.LN.DocsTipoVehiculo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsTipoVehiculo.Editar
{
    public class EditarTipoVehiculoLN : IEditarTipoVehiculoLN
    {
        private readonly IEditarTipoVehiculoAD _editar;
        private readonly IObtenerDatosTipoVehiculoLN _obtenerDatos;

        public EditarTipoVehiculoLN(IEditarTipoVehiculoAD editar, IObtenerDatosTipoVehiculoLN obtenerDatos)
        {
            _editar = editar;
            _obtenerDatos = obtenerDatos;
        }

        public async Task<int> editar(DocsTipoVehiculoDTO tipovehiculoDTO)
        {
            if (tipovehiculoDTO == null)
            {
                return 0;
            }

            try
            {
                int bandera = await _editar.editar(_obtenerDatos.ObtenerDeFront(tipovehiculoDTO));
                return bandera;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarTipoVehiculoLN: {ex.Message}");
                return 0;
            }
        }

    }
}
