using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsInscripcionVehiculo.BuscarXid
{
    public interface IBuscarDocsInscripcionVehiculoLN
    {
        Task<DocsInscripcionVehiculoDTO?> buscar(int id);
    }
}
