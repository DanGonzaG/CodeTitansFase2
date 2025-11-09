using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.BitacoraEventos.BuscarXid
{
    public interface IBuscarEventosAD
    {
        Task<TBitacoraEventos?> BuscarXid(int idEvento);
        Task<List<TBitacoraEventos>> ObtenerTodas();
        Task<List<BitacoraEventosDTO>> ObtenerPorUsuario(string usuario);
    }
}
