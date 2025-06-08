using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CitasTipo.Crear
{
    public interface ICrearCitasTipoAD
    {
        Task<int> crear(TCitasTipo crear);
    }
}

