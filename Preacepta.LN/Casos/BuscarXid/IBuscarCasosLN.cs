using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Casos.BuscarXid
{
    public interface IBuscarCasosLN
    {
        Task<CasoDTO?> buscar(int id);
    }
}
