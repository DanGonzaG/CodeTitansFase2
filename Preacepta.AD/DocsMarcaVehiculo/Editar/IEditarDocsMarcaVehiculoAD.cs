using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsMarcaVehiculo.Editar
{
    public interface IEditarDocsMarcaVehiculoAD
    {
        Task<int> Editar(TDocsMarcaVehiculo editar);
    }
}
