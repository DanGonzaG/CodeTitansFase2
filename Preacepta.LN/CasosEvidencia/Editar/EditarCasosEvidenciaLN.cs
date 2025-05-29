using Preacepta.AD.CasosEtapa.Editar;
using Preacepta.AD.CasosEvidencia.Editar;
using Preacepta.LN.CasosEtapa.ObtenerDatos;
using Preacepta.LN.CasosEvidencia.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CasosEvidencia.Editar
{
    public class EditarCasosEvidenciaLN : IEditarCasosEvidenciaLN
    {
        private readonly IEditarCasosEvidenciaAD _editar;
        private readonly IObtnerDatosCasoEvidenciaLN _obtenerDatosLN;

        public EditarCasosEvidenciaLN(IEditarCasosEvidenciaAD editar,
            IObtnerDatosCasoEvidenciaLN obtenerDatos)
        {
            _editar = editar;
            _obtenerDatosLN = obtenerDatos;
        }

        public async Task<int> Editar(CasosEvidenciaDTO editar)
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
                Console.WriteLine($"Error en EditarCasosEvidenciaLN: {ex.Message}");
                return 0;
            }
        }
    }
}
