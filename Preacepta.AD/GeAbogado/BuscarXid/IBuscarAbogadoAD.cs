using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeAbogado.BuscarXid
{
    public interface IBuscarAbogadoAD
    {
        Task<TGeAbogado?> buscar(int id);
    }
}
