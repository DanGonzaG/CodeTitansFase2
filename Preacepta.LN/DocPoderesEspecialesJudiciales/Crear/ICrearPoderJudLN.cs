using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocPoderesEspecialesJudiciales.Crear
{
    public interface ICrearPoderJudLN
    {
        Task<int> crear(DocsPoderesEspecialesJudicialeDTO poderJudDTO);
    }
}
