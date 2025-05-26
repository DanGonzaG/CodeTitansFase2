using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeAbogadoTipo.BuscarXid
{
    public interface IBuscarAbogadoTipoLN
    {
        Task<GeAbogadoTipoDTO?> buscar(int id);
    }
}
