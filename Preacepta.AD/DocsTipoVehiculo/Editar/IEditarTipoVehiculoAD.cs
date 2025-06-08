using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsTipoVehiculo.Editar
{
    public interface IEditarTipoVehiculoAD
    {
        Task<int> editar(TDocsTipoVehiculo tipovehiculo);
    }
}
