using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsMarcaVehiculo.ObtenerDatos
{
    public class ObtenerDatosDocsMarcaVehiculoLN : IObtenerDatosDocsMarcaVehiculoLN
    {
        public DocsMarcaVehiculoDTO ObtenerDeDB(TDocsMarcaVehiculo datos)
        {
            return new DocsMarcaVehiculoDTO
            {
                Id = datos.Id,
                Nombre = datos.Nombre
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TDocsMarcaVehiculo ObtenerDeFront(DocsMarcaVehiculoDTO datos)
        {
            return new TDocsMarcaVehiculo
            {
                Id = datos.Id,
                Nombre = datos.Nombre
            };
        }
    }
}
