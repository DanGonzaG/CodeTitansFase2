using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsInscripcionVehiculo.Editar
{
    public interface IEditarDocsInscripcionVehiculoLN
    {
        Task<int> Editar(DocsInscripcionVehiculoDTO editar);
    }
}
