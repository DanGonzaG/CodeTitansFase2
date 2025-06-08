using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsPagare.Editar
{
    public interface IEditarPagareAD
    {
        Task<int> editar(TDocsPagare pagare);
    }
}
