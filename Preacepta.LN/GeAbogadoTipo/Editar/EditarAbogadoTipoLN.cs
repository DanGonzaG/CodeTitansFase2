using Preacepta.AD.GeAbogadoTipo.Editar;
using Preacepta.AD.GePersona.Editar;
using Preacepta.LN.GeAbogadoTipo.ObtenerDatos;
using Preacepta.LN.GePersona.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeAbogadoTipo.Editar
{
    public class EditarAbogadoTipoLN : IEditarAbogadoTipoLN
    {
        private readonly IEditarAbogadoTipoAD _editar;
        private readonly IObtenerDatosAbogadoTipoLN _obtenerDatosLN;

        public EditarAbogadoTipoLN(IEditarAbogadoTipoAD editarAbogadoTipoAD, 
            IObtenerDatosAbogadoTipoLN obtenerDatosAbogadoTipoLN)
        {
            _editar = editarAbogadoTipoAD;
            _obtenerDatosLN = obtenerDatosAbogadoTipoLN;
        }

        public async Task<int> editar(GeAbogadoTipoDTO geAbogadoTipoDTO)
        {
            if (geAbogadoTipoDTO == null)
            {
                return 0;
            }

            try
            {
                int bandera = await _editar.editar(_obtenerDatosLN.ObtenerDeFront(geAbogadoTipoDTO));
                return bandera;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarAbogadoTipoLN: {ex.Message}");
                return 0;

            }




        }
    }
}
