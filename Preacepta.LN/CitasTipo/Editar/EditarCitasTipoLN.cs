using Preacepta.AD.CitasTipo.Editar;
using Preacepta.LN.CitasTipo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CitasTipo.Editar

{
    public class EditarCitasTipoLN : IEditarCitasTipoLN
    {
        private readonly IEditarCitasTipoAD _editar;
        private readonly IObtenerDatosCitasTipoLN _obtenerDatosLN;

        public EditarCitasTipoLN(IEditarCitasTipoAD editar,
            IObtenerDatosCitasTipoLN obtenerDatos)
        {
            _editar = editar;
            _obtenerDatosLN = obtenerDatos;
        }

        public async Task<int> editar(CitasTipoDTO editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                int bandera = await _editar.editar(_obtenerDatosLN.ObtenerDeFront(editar));
                return bandera;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarCitasTipoLN: {ex.Message}");
                return 0;

            }




        }
    }
}
