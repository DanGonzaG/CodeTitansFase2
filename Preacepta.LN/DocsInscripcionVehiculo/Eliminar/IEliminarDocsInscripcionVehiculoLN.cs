using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsInscripcionVehiculo.Eliminar
{
    public interface IEliminarDocsInscripcionVehiculoLN
    {
        Task<int> Eliminar(int id);
    }
}
