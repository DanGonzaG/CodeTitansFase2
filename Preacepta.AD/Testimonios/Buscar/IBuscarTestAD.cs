using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Testimonios.Buscar
{
    public interface IBuscarTestAD
    {
        Task<TTestimonio?> buscar(int id);

        Task<TGePersona?> BuscarPorId(int idCliente);
    }
}
