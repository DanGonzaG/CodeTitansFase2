using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CasosEtapa.BuscarXid
{
    public interface IBuscarCasosEtapasLN
    {
        Task<CasosEtapaDTO?> buscar(int id);
    }
}
