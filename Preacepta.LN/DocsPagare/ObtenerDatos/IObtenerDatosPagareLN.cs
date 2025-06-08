using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsPagare.ObtenerDatos
{
    public interface IObtenerDatosPagareLN
    {
        DocsPagareDTO ObtenerDeDB(TDocsPagare pagare);
        TDocsPagare ObtenerDeFront(DocsPagareDTO pagareDTO);
    }
}
