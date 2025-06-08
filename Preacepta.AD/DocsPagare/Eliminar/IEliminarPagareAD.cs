using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsPagare.Eliminar
{
    public interface IEliminarPagareAD
    {
        Task<int> eliminar(int id);
    }
}
