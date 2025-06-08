using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocPoderesEspecialesJudiciales.Buscar
{
    public interface IBuscarPoderJudLN
    {
        Task<DocsPoderesEspecialesJudicialeDTO?> buscar(int id);
    }
}
