using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CrDireccion1.Listar
{
    public interface IListarCrDireccion1AD
    {
        Task<List<CrProvinciaDTO>> listarProvincias();
        Task<List<CrCantonDTO>> listarCantones();
        Task<List<CrDistritoDTO>> listarDistritos();
    }
}
