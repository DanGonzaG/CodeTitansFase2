using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CasosTipo.ObtenerDatos
{
    public interface IObtenerDatosCasosTipoLN
    {
        CasosTipoDTO ObtenerDeDB(TCasosTipo baseDatos);
        TCasosTipo ObtenerDeFront(CasosTipoDTO Formulario);
    }
}
