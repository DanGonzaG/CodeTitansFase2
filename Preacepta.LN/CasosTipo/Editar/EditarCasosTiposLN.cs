using Preacepta.AD.CasosTipo.Editar;
using Preacepta.LN.CasosTipo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosTipo.Editar
{
    public class EditarCasosTiposLN : IEditarCasosTiposLN
    {
        private readonly IEditarCasosTiposAD _editar;
        private readonly IObtenerDatosCasosTipoLN _obtenerDatosLN;

        public EditarCasosTiposLN(IEditarCasosTiposAD editar,
            IObtenerDatosCasosTipoLN obtenerDatos)
        {
            _editar = editar;
            _obtenerDatosLN = obtenerDatos;
        }

        public async Task<int> Editar(CasosTipoDTO editar)
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
                Console.WriteLine($"Error en EditarCasosTiposLN: {ex.Message}");
                return 0;
            }
        }
    }
}
