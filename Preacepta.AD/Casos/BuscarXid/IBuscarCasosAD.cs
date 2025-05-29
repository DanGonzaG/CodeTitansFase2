using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Casos.BuscarXid
{
    public interface IBuscarCasosAD
    {
        Task<TCaso?> buscar(int id);
    }
}
