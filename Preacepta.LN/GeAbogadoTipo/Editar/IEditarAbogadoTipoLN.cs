using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeAbogadoTipo.Editar
{
    public interface IEditarAbogadoTipoLN
    {
        Task<int> editar(GeAbogadoTipoDTO geAbogadoTipoDTO);
    }
}
