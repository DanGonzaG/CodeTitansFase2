using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeRedesSociales.BuscarXid
{
    public interface IBuscarRedesSocialesAD
    {
        Task<TGeRedesSociale?> buscar(int id);
    }
}
