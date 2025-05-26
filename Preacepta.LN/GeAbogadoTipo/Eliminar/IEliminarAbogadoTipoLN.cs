using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeAbogadoTipo.Eliminar
{
    public interface IEliminarAbogadoTipoLN
    {
        Task<int> eliminar(int id);
    }
}
