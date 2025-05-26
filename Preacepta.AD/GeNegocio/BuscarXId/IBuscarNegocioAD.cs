using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeNegocio.BuscarXId
{
    public interface IBuscarNegocioAD
    {
        Task<TGeNegocio?> buscar(int id);
    }
}
