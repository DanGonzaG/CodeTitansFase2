using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Casos.Eliminar
{
    public interface IELiminarCasosAD
    {
        Task<int> Eliminar(int id);
    }
}
