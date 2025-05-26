using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GePersona.BuscarXid
{
    public interface IBuscarXidGePersonaLN
    {
        Task<GePersonaDTO?> buscar(int id);
    }
}
