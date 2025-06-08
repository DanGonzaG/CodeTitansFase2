using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsPagare.Editar
{
    public interface IEditarPagareLN
    {
        Task<int> editar(DocsPagareDTO pagareDTO);
    }
}
