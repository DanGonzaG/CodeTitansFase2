using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsTipoVehiculo.ObtenerDatos
{
    public interface IObtenerDatosTipoVehiculoLN
    {
        DocsTipoVehiculoDTO ObtenerDeDB(TDocsTipoVehiculo tipovehiculo);
        TDocsTipoVehiculo ObtenerDeFront(DocsTipoVehiculoDTO tipovehiculoDTO);
    }
}
