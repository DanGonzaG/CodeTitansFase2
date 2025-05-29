using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Casos.ObtenerDatos
{
    public interface IObtenerDatosCasoLN
    {
        CasoDTO ObtenerDeDB(TCaso datos);
        TCaso ObtenerDeFront(CasoDTO datos);
    }
}
