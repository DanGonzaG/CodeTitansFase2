using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeAbogado.Eliminar
{
    public interface IEliminarAbogadoLN
    {
        Task<int> Eliminar(int id);
    }
}
