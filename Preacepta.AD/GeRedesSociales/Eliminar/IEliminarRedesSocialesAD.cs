using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeRedesSociales.Eliminar
{
    public interface IEliminarRedesSocialesAD
    {
        Task<int> Eliminar(int id);
    }
}
