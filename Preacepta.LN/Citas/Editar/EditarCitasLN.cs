using Preacepta.AD.Citas.Editar;
using Preacepta.LN.Citas.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Citas.Editar

{
    public class EditarCitasLN : IEditarCitasLN
    {
        private readonly IEditarCitasAD _editar;
        private readonly IObtenerDatosCitasLN _obtenerDatosLN;

        public EditarCitasLN(IEditarCitasAD editar,
            IObtenerDatosCitasLN obtenerDatos)
        {
            _editar = editar;
            _obtenerDatosLN = obtenerDatos;
        }

        public async Task<int> editar(CitasDTO editar)
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
                Console.WriteLine($"Error en EditarCitasLN: {ex.Message}");
                return 0;

            }




        }
    }
}
