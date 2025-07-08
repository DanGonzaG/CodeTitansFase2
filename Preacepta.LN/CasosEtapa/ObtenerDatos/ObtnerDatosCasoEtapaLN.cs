using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.CasosEtapa.ObtenerDatos
{
    public class ObtnerDatosCasoEtapaLN : IObtnerDatosCasoEtapaLN
    {
        public CasosEtapaDTO ObtenerDeDB(TCasosEtapa datos)
        {
            return new CasosEtapaDTO
            {
                IdEtapaPl = datos.IdEtapaPl,
                Nombre = datos.Nombre,
                Fecha = datos.Fecha.ToString("dd-MM-yyyy"),
                Descripcion = datos.Descripcion,
                IdCaso = datos.IdCaso,
                IdCasoNavigation = datos.IdCasoNavigation,
                Activo = datos.Activo
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TCasosEtapa ObtenerDeFrontCrear(CasosEtapaDTO datos)
        {
            return new TCasosEtapa
            {
                IdEtapaPl = datos.IdEtapaPl,
                Nombre = datos.Nombre,
                Fecha = DateTime.Now,
                Descripcion = datos.Descripcion,
                IdCaso = datos.IdCaso,
                IdCasoNavigation = datos.IdCasoNavigation,
                Activo = true
            };
        }

        public TCasosEtapa ObtenerDeFrontEditar(CasosEtapaDTO datos)
        {
            return new TCasosEtapa
            {
                IdEtapaPl = datos.IdEtapaPl,
                Nombre = datos.Nombre,
                Fecha = DateTime.Parse(datos.Fecha),
                Descripcion = datos.Descripcion,
                IdCaso = datos.IdCaso,
                IdCasoNavigation = datos.IdCasoNavigation,
                Activo = datos.Activo
            };
        }
    }
}
