using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsTipoVehiculo.Editar
{
    public interface IEditarTipoVehiculoLN
    {
        Task<int> editar(DocsTipoVehiculoDTO tipovehiculoDTO);
    }
}
