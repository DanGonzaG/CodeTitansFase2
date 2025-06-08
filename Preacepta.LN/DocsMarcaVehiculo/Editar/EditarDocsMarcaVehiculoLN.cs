using Preacepta.AD.CasosTipo.Editar;
using Preacepta.AD.DocsMarcaVehiculo.Editar;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.DocsMarcaVehiculo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsMarcaVehiculo.Editar
{
    public class EditarDocsMarcaVehiculoLN : IEditarDocsMarcaVehiculoLN
    {
        private readonly IEditarDocsMarcaVehiculoAD _editar;
        private readonly IObtenerDatosDocsMarcaVehiculoLN _obtenerDatosLN;

        public EditarDocsMarcaVehiculoLN(IEditarDocsMarcaVehiculoAD editar,
            IObtenerDatosDocsMarcaVehiculoLN obtenerDatos)
        {
            _editar = editar;
            _obtenerDatosLN = obtenerDatos;
        }

        public async Task<int> Editar(DocsMarcaVehiculoDTO editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                int bandera = await _editar.Editar(_obtenerDatosLN.ObtenerDeFront(editar));
                return bandera;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarDocsMarcaVehiculoLN: {ex.Message}");
                return 0;
            }
        }
    }
}
