using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeAbogadoTipo.Crear
{
    public interface ICrearAbogadoTipoAD
    {
        Task<int> crear(TGeAbogadoTipo geAbogadoTipo);
    }
}
