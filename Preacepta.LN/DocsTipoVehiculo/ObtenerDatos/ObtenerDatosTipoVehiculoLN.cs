using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsTipoVehiculo.ObtenerDatos
{
    public class ObtenerDatosTipoVehiculoLN : IObtenerDatosTipoVehiculoLN
    {
        public DocsTipoVehiculoDTO ObtenerDeDB(TDocsTipoVehiculo tipovehiculo)
        {
            return new DocsTipoVehiculoDTO
            {
                Id = tipovehiculo.Id,
                Nombre = tipovehiculo.Nombre,
                TDocsInscripcionVehiculos = tipovehiculo.TDocsInscripcionVehiculos,
                TDocsOpcionCompraventaVehiculos = tipovehiculo.TDocsOpcionCompraventaVehiculos
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TDocsTipoVehiculo ObtenerDeFront(DocsTipoVehiculoDTO tipovehiculoDTO)
        {
            return new TDocsTipoVehiculo
            {
                Id = tipovehiculoDTO.Id,
                Nombre = tipovehiculoDTO.Nombre,
                TDocsInscripcionVehiculos = tipovehiculoDTO.TDocsInscripcionVehiculos,
                TDocsOpcionCompraventaVehiculos = tipovehiculoDTO.TDocsOpcionCompraventaVehiculos
            };
        }
    }
}
