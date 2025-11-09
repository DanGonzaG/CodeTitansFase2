using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.BitacoraEventos.BuscarXid
{
   public interface IBuscarEventosLN
    {
        Task<TBitacoraEventos?> BuscarXid(int Id_evento);
    }
}
