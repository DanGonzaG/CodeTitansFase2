using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocPoderesEspecialesJudiciales.Eliminar
{
    public interface IEliminarPoderJudLN
    {
        Task<int> eliminar(int id);
    }
}
