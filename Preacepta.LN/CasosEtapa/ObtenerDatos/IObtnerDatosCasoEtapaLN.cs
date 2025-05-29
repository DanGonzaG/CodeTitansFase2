using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CasosEtapa.ObtenerDatos
{
    public interface IObtnerDatosCasoEtapaLN
    {
        CasosEtapaDTO ObtenerDeDB(TCasosEtapa datos);
        TCasosEtapa ObtenerDeFront(CasosEtapaDTO datos);
    }
}
