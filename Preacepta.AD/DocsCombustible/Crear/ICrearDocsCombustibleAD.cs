using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsCombustible.Crear
{
    public interface ICrearDocsCombustibleAD
    {
        Task<int> crear(TDocsCombustible crear);
    }
}
