using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsPagare.Crear
{
    public interface ICrearPagareAD
    {
        Task<int> crear(TDocsPagare pagare);    
    }
}
