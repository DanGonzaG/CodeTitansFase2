using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsContratoPrestacionServicios.Eliminar
{
    public interface IEliminarDocsContratoPrestacionServiciosAD
    {
        Task<int> Eliminar(int id);
    }
}
