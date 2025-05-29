using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Casos.Listar
{
    public interface IListarCasosAD
    {
        Task<List<CasoDTO>> listar();
    }
}
