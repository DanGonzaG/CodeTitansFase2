using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeAbogadoTipo.BuscarXid
{
    public interface IBuscarAbogadoTipoAD
    {
        Task<TGeAbogadoTipo?> buscar(int id);
    }
}
