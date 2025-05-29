using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeRedesSociales.Eliminar
{
    public interface IEliminarRedesSocialesLN
    {
        Task<int> Eliminar(int id);
    }
}
