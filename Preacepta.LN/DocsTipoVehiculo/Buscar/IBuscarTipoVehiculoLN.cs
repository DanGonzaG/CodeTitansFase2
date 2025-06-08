using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsTipoVehiculo.Buscar
{
    public interface IBuscarTipoVehiculoLN
    {
        Task<DocsTipoVehiculoDTO?> buscar(int id);
    }
}
