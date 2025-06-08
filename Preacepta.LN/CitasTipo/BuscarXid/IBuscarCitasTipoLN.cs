using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CitasTipo.BuscarXid
{
    public interface IBuscarCitasTipoLN
    {
        Task<CitasTipoDTO?> buscar(int id);
    }
}
