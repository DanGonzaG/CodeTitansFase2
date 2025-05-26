using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CasosTipo.Listar
{
    public interface IListarCasosTipoLN
    {
        Task<List<CasosTipoDTO>> listar();
    }
}
