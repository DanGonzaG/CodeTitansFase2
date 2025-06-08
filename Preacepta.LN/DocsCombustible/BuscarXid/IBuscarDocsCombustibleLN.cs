using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsCombustible.BuscarXid
{
    public interface IBuscarDocsCombustibleLN
    {
        Task<DocsCombustibleDTO?> buscar(int id);
    }
}
