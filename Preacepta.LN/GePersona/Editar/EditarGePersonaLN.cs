using Preacepta.AD.GePersona.Editar;
using Preacepta.LN.GePersona.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GePersona.Editar
{
    public class EditarGePersonaLN : IEditarGePersonaLN
    {
        private readonly IEditarGePersonaAD _editarGePersonaAD;
        private readonly IObtenerDatosLN _obtenerDatosLN;

        public EditarGePersonaLN(IEditarGePersonaAD editarGePersonaAD, IObtenerDatosLN obtenerDatosLN)
        {
            _editarGePersonaAD = editarGePersonaAD;
            _obtenerDatosLN = obtenerDatosLN;
        }

        public async Task<int> editar(GePersonaDTO gePersonaDTO)
        {
            if (gePersonaDTO == null)
            {
                return 0;
            }

            try
            {
                int bandera = await _editarGePersonaAD.editar(_obtenerDatosLN.ObtenerDeFrontEditar(gePersonaDTO));
                return bandera;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarGepersonaLN: {ex.Message}");
                return 0;

            }




        }
    }
}
