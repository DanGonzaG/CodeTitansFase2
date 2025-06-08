using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsMarcaVehiculo.Editar
{
    public interface IEditarDocsMarcaVehiculoLN
    {
        Task<int> Editar(DocsMarcaVehiculoDTO editar);
    }
}
