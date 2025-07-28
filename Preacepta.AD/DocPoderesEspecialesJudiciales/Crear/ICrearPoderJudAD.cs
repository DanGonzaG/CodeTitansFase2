using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocPoderesEspecialesJudiciales.Crear
{
    public interface ICrearPoderJudAD
    {
        Task<int> crear(TDocsPoderesEspecialesJudiciale poderJud);
    }
}
