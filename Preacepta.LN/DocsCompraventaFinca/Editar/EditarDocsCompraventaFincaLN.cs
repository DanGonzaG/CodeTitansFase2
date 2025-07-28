using Preacepta.AD.CasosTipo.Editar;
using Preacepta.AD.DocsCompraventaFinca.Editar;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.DocsCompraventaFinca.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCompraventaFinca.Editar
{
    public class EditarDocsCompraventaFincaLN : IEditarDocsCompraventaFincaLN
    {
        private readonly IEditarDocsCompraventaFincaAD _editar;
        private readonly IObtenerDatosDocsCompraventaFincaLN _obtenerDatosLN;

        public EditarDocsCompraventaFincaLN(IEditarDocsCompraventaFincaAD editar,
            IObtenerDatosDocsCompraventaFincaLN obtenerDatos)
        {
            _editar = editar;
            _obtenerDatosLN = obtenerDatos;
        }

        public async Task<int> Editar(DocsCompraventaFincaDTO editar)
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
                Console.WriteLine($"Error en EditarDocsCompraventaFincaLN: {ex.Message}");
                return 0;
            }
        }
    }
}
