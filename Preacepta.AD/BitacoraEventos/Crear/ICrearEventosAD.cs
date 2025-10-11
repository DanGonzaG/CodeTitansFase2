using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.BitacoraEventos.Crear
{
    public interface ICrearEventosAD
    {
        Task<int> InsertarEvento(TBitacoraEventos evento);
    }
}
