using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsInscripcionVehiculo.Crear
{
    public interface ICrearDocsInscripcionVehiculoAD
    {
        Task<int> crear(TDocsInscripcionVehiculo crear);
    }
}
