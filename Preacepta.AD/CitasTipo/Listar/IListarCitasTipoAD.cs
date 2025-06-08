using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CitasTipo.Listar
{
    public interface IListarCitasTipoAD
    {
        Task<List<CitasTipoDTO>> listar();
    }
}
