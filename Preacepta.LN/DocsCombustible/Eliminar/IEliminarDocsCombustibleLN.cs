using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCombustible.Eliminar
{
    public interface IEliminarDocsCombustibleLN
    {
        Task<int> Eliminar(int id);
    }
}
