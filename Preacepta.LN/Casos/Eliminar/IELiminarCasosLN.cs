using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Casos.Eliminar
{
    public interface IELiminarCasosLN
    {
        Task<int> Eliminar(int id);
    }
}
