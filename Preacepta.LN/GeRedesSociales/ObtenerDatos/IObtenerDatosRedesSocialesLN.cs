using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeRedesSociales.ObtenerDatos
{
    public interface IObtenerDatosRedesSocialesLN
    {
        GeRedesSocialeDTO ObtenerDeDB(TGeRedesSociale datos);
        TGeRedesSociale ObtenerDeFront(GeRedesSocialeDTO datos);
    }
}
