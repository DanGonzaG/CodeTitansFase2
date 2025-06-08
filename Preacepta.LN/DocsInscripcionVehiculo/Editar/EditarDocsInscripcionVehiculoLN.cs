using Preacepta.AD.CasosTipo.Editar;
using Preacepta.AD.DocsInscripcionVehiculo.Editar;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.DocsInscripcionVehiculo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsInscripcionVehiculo.Editar
{
    public class EditarDocsInscripcionVehiculoLN : IEditarDocsInscripcionVehiculoLN
    {
        private readonly IEditarDocsInscripcionVehiculoAD _editar;
        private readonly IObtenerDatosDocsInscripcionVehiculoTipoLN _obtenerDatosLN;

        public EditarDocsInscripcionVehiculoLN(IEditarDocsInscripcionVehiculoAD editar,
            IObtenerDatosDocsInscripcionVehiculoTipoLN obtenerDatos)
        {
            _editar = editar;
            _obtenerDatosLN = obtenerDatos;
        }

        public async Task<int> Editar(DocsInscripcionVehiculoDTO editar)
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
                Console.WriteLine($"Error en EditarDocsInscripcionVehiculoLN: {ex.Message}");
                return 0;
            }
        }
    }
}
