using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CasosEvidencia.BuscarXid
{
    public interface IBuscarCasosEvidenciaAD
    {
        Task<TCasosEvidencia?> buscar(int id);
    }
}
