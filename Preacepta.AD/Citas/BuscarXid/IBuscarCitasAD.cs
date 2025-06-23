using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Citas.BuscarXid
{
    public interface IBuscarCitasAD
    {
        Task<TCita?> buscar(int id);
        Task<List<TCita>> obtenerTodas();

    }
}
