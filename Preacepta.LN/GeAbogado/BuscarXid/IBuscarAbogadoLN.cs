using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeAbogado.BuscarXid
{
    public interface IBuscarAbogadoLN
    {
        Task<GeAbogadoDTO?> buscar(int id);
    }
}
