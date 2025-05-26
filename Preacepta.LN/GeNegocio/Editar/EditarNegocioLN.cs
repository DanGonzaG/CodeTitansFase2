using Preacepta.AD.CasosTipo.Editar;
using Preacepta.AD.GeNegocio.Editar;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.LN.GeNegocio.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeNegocio.Editar
{
    public class EditarNegocioLN : IEditarNegocioLN
    {
        private readonly IEditarNegocioAD _editar;
        private readonly IObtenerDatosNegocioLN _obtenerDatosLN;

        public EditarNegocioLN(IEditarNegocioAD editar,
            IObtenerDatosNegocioLN obtenerDatos)
        {
            _editar = editar;
            _obtenerDatosLN = obtenerDatos;
        }

        public async Task<int> Editar(GeNegocioDTO editar)
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
                Console.WriteLine($"Error en EditarNegocioLN: {ex.Message}");
                return 0;

            }




        }
    }
}
