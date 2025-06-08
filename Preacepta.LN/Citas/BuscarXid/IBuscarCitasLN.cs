using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Citas.BuscarXid
{
    public interface IBuscarCitasLN
    {
        Task<CitasDTO?> buscar(int id);
    }
}
