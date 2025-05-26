using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeNegocio.Eliminar
{
    public interface IEliminarNegocioLN
    {
        Task<int> Eliminar(int id);
    }
}
