using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsTipoVehiculo.Listar
{
    public interface IListarTipoVehiculoAD
    {
        Task<List<DocsTipoVehiculoDTO>> Listar();
    }
}
