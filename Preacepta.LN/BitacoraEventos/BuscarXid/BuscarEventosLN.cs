
using Preacepta.AD;
using Preacepta.AD.BitacoraEventos.BuscarXid;
using Preacepta.LN.BitacoraEventos.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Preacepta.LN.BitacoraEventos.BuscarXid
{
    public class BuscarEventosLN : IBuscarEventosLN
    {
        private readonly IBuscarEventosAD _buscarEventosAD;
        public BuscarEventosLN(IBuscarEventosAD buscarEventosAD)
        {
            _buscarEventosAD = buscarEventosAD;
        }
        public async Task<TBitacoraEventos?> BuscarXid(int Id_evento)
        {
            if (Id_evento == 0) return null;

            return await _buscarEventosAD.BuscarXid(Id_evento);
        }
    }
}
