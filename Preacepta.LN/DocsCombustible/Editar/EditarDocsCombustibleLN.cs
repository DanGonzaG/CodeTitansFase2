using Preacepta.AD.CasosTipo.Editar;
using Preacepta.AD.DocsCombustible.Editar;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.DocsCombustible.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCombustible.Editar
{
    public class EditarDocsCombustibleLN : IEditarDocsCombustibleLN
    {
        private readonly IEditarDocsCombustibleAD _editar;
        private readonly IObtenerDatosDocsCombustibleLN _obtenerDatosLN;

        public EditarDocsCombustibleLN(IEditarDocsCombustibleAD editar,
            IObtenerDatosDocsCombustibleLN obtenerDatos)
        {
            _editar = editar;
            _obtenerDatosLN = obtenerDatos;
        }

        public async Task<int> Editar(DocsCombustibleDTO editar)
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
                Console.WriteLine($"Error en EditarDocsCombustibleLN: {ex.Message}");
                return 0;
            }
        }
    }
}
