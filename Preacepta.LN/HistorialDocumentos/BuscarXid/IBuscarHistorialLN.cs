using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.HistorialDocumentos.BuscarXid
{
    public interface IBuscarHistorialLN
    {
        Task<HistorialDocumentoDTO?> Buscar(int id);
        Task<HistorialDocumentoDTO?> BuscarPorDocumento(string tipoDocumento, int idDocumento);
        Task<int> BuscarXidDocumento(int idDocumento, string nombreTipoDocumento);
    }
}
