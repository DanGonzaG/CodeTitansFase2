using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocPoderesEspecialesJudiciales.ObtenerDatos
{
    public interface IObtenerDatosPoderJudLN
    {
        DocsPoderesEspecialesJudicialeDTO ObtenerDeDB(TDocsPoderesEspecialesJudiciale poderJud);
        TDocsPoderesEspecialesJudiciale ObtenerDeFront(DocsPoderesEspecialesJudicialeDTO poderJudDTO);

    }
}
