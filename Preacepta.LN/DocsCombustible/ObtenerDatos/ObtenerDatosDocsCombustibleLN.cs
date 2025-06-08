using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCombustible.ObtenerDatos
{
    public class ObtenerDatosDocsCombustibleLN : IObtenerDatosDocsCombustibleLN
    {
        public DocsCombustibleDTO ObtenerDeDB(TDocsCombustible datos)
        {
            return new DocsCombustibleDTO
            {
                Id = datos.Id,
                Nombre = datos.Nombre,
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TDocsCombustible ObtenerDeFront(DocsCombustibleDTO datos)
        {
            return new TDocsCombustible
            {
                Id = datos.Id,
                Nombre = datos.Nombre,
            };
        }
    }
}
