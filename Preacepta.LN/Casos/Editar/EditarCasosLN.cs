using Preacepta.AD.Casos.Editar;
using Preacepta.LN.Casos.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.Casos.Editar
{
    public class EditarCasosLN : IEditarCasosLN
    {
        private readonly IEditarCasosAD _editar;
        private readonly IObtenerDatosCasoLN _obtenerDatosLN;

        public EditarCasosLN(IEditarCasosAD editar,
            IObtenerDatosCasoLN obtenerDatos)
        {
            _editar = editar;
            _obtenerDatosLN = obtenerDatos;
        }

        public async Task<int> Editar(CasoDTO editar)
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
                Console.WriteLine($"Error en EditarCasosLN: {ex.Message}");
                return 0;
            }
        }
    }
}
