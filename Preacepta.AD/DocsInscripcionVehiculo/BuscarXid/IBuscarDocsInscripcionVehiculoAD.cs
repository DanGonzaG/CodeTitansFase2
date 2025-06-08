using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsInscripcionVehiculo.BuscarXid
{
    public interface IBuscarDocsInscripcionVehiculoAD
    {
        Task<TDocsInscripcionVehiculo?> buscar(int id);
    }
}
