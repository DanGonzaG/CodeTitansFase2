using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.HistorialDocumentos.Eliminar
{
    public interface IELiminarHistorialAD
    {
        Task<int> Eliminar(int id);
    }
}
