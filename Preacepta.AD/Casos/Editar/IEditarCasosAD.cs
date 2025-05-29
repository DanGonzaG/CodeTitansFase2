using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Casos.Editar
{
    public interface IEditarCasosAD
    {
        Task<int> Editar(TCaso editar);
    }
}
