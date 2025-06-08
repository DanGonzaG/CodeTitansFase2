using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeAbogadoTipo.ObtenerDatos
{
    public class ObtenerDatosAbogadoTipoLN : IObtenerDatosAbogadoTipoLN
    {
        /*metodo para usarlo en los vistas de detalles o deatails o buscar por id*/
        public GeAbogadoTipoDTO ObtenerDeDB(TGeAbogadoTipo geAbogadoTipo)
        {
            return new GeAbogadoTipoDTO
            {
                IdTipoAbogado = geAbogadoTipo.IdTipoAbogado,
                Nombre = geAbogadoTipo.Nombre,
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TGeAbogadoTipo ObtenerDeFront(GeAbogadoTipoDTO geAbogadoTipoDTO)
        {
            return new TGeAbogadoTipo
            {
                IdTipoAbogado = geAbogadoTipoDTO.IdTipoAbogado,
                Nombre = geAbogadoTipoDTO.Nombre,
            };
        }
    }
}
