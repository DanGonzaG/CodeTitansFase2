using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Citas.Editar
{
    public interface IEditarCitasLN
    {
        Task<int> editar(CitasDTO editar);
    }
}
