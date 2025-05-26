using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeNegocio.BuscarXId
{
    public interface IBuscarNegocioLN
    {
        Task<GeNegocioDTO?> buscar(int id);
    }
}
