using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeNegocio.ObtenerDatos
{
    public interface IObtenerDatosNegocioLN
    {
        GeNegocioDTO ObtenerDeDB(TGeNegocio datos);
        TGeNegocio ObtenerDeFront(GeNegocioDTO datos);
    }
}
