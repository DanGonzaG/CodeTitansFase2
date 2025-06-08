using Preacepta.AD.DocsOpcionCompraventaVehiculo.Editar;
using Preacepta.LN.DocsOpcionCompraventaVehiculo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsOpcionCompraventaVehiculo.Editar
{
    public class EditarDocCVLN : IEditarDocCVLN
    {
        private readonly IEditarDocCVAD _editarDocCVAD;
        private readonly IObtenerDatosDocsCV _obtenerDatosDocsCV;

        public EditarDocCVLN(IEditarDocCVAD editarDocCVAD, IObtenerDatosDocsCV obtenerDatosDocsCV)
        {
            _editarDocCVAD = editarDocCVAD;
            _obtenerDatosDocsCV = obtenerDatosDocsCV;
        }

        public async Task<int> editar(DocsOpcionCompraventaVehiculoDTO docCVDTO)
        {
            if (docCVDTO == null)
            {
                return 0;
            }

            try
            {
                int bandera = await _editarDocCVAD.editar(_obtenerDatosDocsCV.ObtenerDeFront(docCVDTO));
                return bandera;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarDocCVLN: {ex.Message}");
                return 0;
            }
        }

    }
}
