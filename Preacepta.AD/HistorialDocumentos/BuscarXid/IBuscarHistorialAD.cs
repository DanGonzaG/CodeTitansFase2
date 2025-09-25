using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.HistorialDocumentos.BuscarXid
{
    public interface IBuscarHistorialAD
    {
        Task<THistorialDocumento?> Buscar(int id);
        Task<THistorialDocumento?> BuscarPorDocumento(string tipoDocumento, int idDocumento);
        Task<int> BuscarXidDocumento(int idDocumento, string nombreTipoDocumento);
    }
}
