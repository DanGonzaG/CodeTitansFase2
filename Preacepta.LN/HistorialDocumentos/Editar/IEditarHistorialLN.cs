using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.HistorialDocumentos.Editar
{
    public interface IEditarHistorialLN
    {
        Task<int> Editar(HistorialDocumentoDTO editar);
    }
}
