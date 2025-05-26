using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CasosTipo.Listar
{
    public interface IListarCasosTipoAD
    {
        Task<List<CasosTipoDTO>> listar();
    }
}
