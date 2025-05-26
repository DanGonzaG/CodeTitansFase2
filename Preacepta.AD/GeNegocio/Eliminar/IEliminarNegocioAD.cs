using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeNegocio.Eliminar
{
    public interface IEliminarNegocioAD
    {
        Task<int> Eliminar(int id);
    }
}
