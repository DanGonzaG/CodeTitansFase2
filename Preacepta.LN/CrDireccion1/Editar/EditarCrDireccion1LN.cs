using Preacepta.AD.CrDireccion1.Editar;
using Preacepta.LN.CrDireccion1.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CrDireccion1.Editar
{
    public class EditarCrDireccion1LN : IEditarCrDireccion1LN
    {
        private readonly IEditarCrDireccion1AD _editar;
        private readonly IObtenerDatosDireccion1LN _obtenerDatosLN;

        public EditarCrDireccion1LN(IEditarCrDireccion1AD editar,
            IObtenerDatosDireccion1LN obtenerDatos)
        {
            _editar = editar;
            _obtenerDatosLN = obtenerDatos;
        }

        public async Task<int> EditarProvincia(CrProvinciaDTO editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                int bandera = await _editar.EditarProvincia(_obtenerDatosLN.ObtenerDeFrontProvincias(editar));
                return bandera;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarCrDireccion1LN - EditarProvincia: {ex.Message}");
                return 0;
            }
        }

        public async Task<int> EditarCanton(CrCantonDTO editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                int bandera = await _editar.EditarCanton(_obtenerDatosLN.ObtenerDeFrontCanton(editar));
                return bandera;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarCrDireccion1LN - EditarCanton: {ex.Message}");
                return 0;
            }
        }

        public async Task<int> EditarDistrito(CrDistritoDTO editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                int bandera = await _editar.EditarDistrito(_obtenerDatosLN.ObtenerDeFrontDistrito(editar));
                return bandera;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarCrDireccion1LN - EditarDistrito: {ex.Message}");
                return 0;
            }
        }
    }
}
