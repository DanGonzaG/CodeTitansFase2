using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CitasTipo.BuscarXid
{
    public interface IBuscarCitasTipoAD
    {
        Task<TCitasTipo?> buscar(int id);
    }
}
