using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Citas.Editar
{
    public interface IEditarCitasAD
    {
        Task<int> editar(TCita editar);
    }
}