using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public TCasosEtapa ObtenerDeFront(CasosEtapaDTO datos)
        {
            return new TCasosEtapa
            {
                IdEtapaPl = datos.IdEtapaPl,
                Nombre = datos.Nombre,
                Fecha = DateTime.Now,
                Descripcion = datos.Descripcion,
                IdCaso = datos.IdCaso,
                IdCasoNavigation = datos.IdCasoNavigation,
                Activo = datos.Activo
            };
        }
    }
}
