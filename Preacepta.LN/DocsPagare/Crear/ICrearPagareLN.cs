using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsPagare.Crear
{
    public interface ICrearPagareLN
    {
        Task<int> crear(DocsPagareDTO pagareDTO);
    }
}
