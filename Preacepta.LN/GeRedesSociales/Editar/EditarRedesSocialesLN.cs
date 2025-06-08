using Preacepta.AD.GeRedesSociales.Editar;
using Preacepta.LN.GeRedesSociales.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeRedesSociales.Editar
{
    public class EditarRedesSocialesLN : IEditarRedesSocialesLN
    {
        private readonly IEditarRedesSocialesAD _editar;
        private readonly IObtenerDatosRedesSocialesLN _obtenerDatosLN;

        public EditarRedesSocialesLN(IEditarRedesSocialesAD editar,
            IObtenerDatosRedesSocialesLN obtenerDatos)
        {
            _editar = editar;
            _obtenerDatosLN = obtenerDatos;
        }

        public async Task<int> Editar(GeRedesSocialeDTO editar)
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
                Console.WriteLine($"Error en EditarRedesSocialesLN: {ex.Message}");
                return 0;
            }
        }
    }
}
