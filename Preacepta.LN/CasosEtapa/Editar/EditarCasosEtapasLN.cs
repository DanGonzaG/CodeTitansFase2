using Preacepta.AD.CasosEtapa.Editar;
using Preacepta.LN.CasosEtapa.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosEtapa.Editar
{
    public class EditarCasosEtapasLN : IEditarCasosEtapasLN
    {
        private readonly IEditarCasosEtapasAD _editar;
        private readonly IObtnerDatosCasoEtapaLN _obtenerDatosLN;

        public EditarCasosEtapasLN(IEditarCasosEtapasAD editar,
            IObtnerDatosCasoEtapaLN obtenerDatos)
        {
            _editar = editar;
            _obtenerDatosLN = obtenerDatos;
        }

        public async Task<int> Editar(CasosEtapaDTO editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                int bandera = await _editar.Editar(_obtenerDatosLN.ObtenerDeFrontEditar(editar));
                return bandera;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarCasosTiposLN: {ex.Message}");
                return 0;
            }
        }
    }
}
