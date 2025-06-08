using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsPagare.Listar
{
    public interface IListarPagareAD
    {
        Task<List<DocsPagareDTO>> Listar();
    }
}
