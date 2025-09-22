using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.HistorialDocumentos.Eliminar
{
    public interface IELiminarHistorialLN
    {
        Task<int> Eliminar(int id);
    }
}
