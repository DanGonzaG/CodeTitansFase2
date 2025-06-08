using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsInscripcionVehiculo.Eliminar
{
    public interface IEliminarDocsInscripcionVehiculoAD
    {
        Task<int> Eliminar(int id);
    }
}
