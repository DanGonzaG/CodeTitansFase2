using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocPoderesEspecialesJudiciales.Editar
{
    public interface IEditarPoderJudLN
    {
        Task<int> editar(DocsPoderesEspecialesJudicialeDTO poderJudDTO);
    }
}
