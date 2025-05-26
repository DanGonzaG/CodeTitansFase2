using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CasosTipo.BuscarXid
{
    public interface IBuscarCasosTiposAD
    {
        Task<TCasosTipo?> buscar(int id);
    }
}
