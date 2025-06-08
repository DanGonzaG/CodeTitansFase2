using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCombustible.ObtenerDatos
{
    public interface IObtenerDatosDocsCombustibleLN
    {
        DocsCombustibleDTO ObtenerDeDB(TDocsCombustible datos);
        TDocsCombustible ObtenerDeFront(DocsCombustibleDTO datos);
    }
}
