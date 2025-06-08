using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsMarcaVehiculo.ObtenerDatos
{
    public interface IObtenerDatosDocsMarcaVehiculoLN
    {
        DocsMarcaVehiculoDTO ObtenerDeDB(TDocsMarcaVehiculo datos);
        TDocsMarcaVehiculo ObtenerDeFront(DocsMarcaVehiculoDTO datos);
    }
}
