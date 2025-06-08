using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsInscripcionVehiculo.ObtenerDatos
{
    public interface IObtenerDatosDocsInscripcionVehiculoTipoLN
    {
        DocsInscripcionVehiculoDTO ObtenerDeDB(TDocsInscripcionVehiculo datos);
        TDocsInscripcionVehiculo ObtenerDeFront(DocsInscripcionVehiculoDTO datos);
    }
}
