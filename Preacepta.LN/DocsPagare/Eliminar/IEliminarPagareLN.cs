using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsPagare.Eliminar
{
    public interface IEliminarPagareLN
    {
        Task<int> eliminar(int id);
    }
}
