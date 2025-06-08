using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsCombustible.Eliminar
{
    public interface IEliminarDocsCombustibleAD
    {
        Task<int> Eliminar(int id);
    }
}
