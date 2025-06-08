using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsContratoPrestacionServicios.Eliminar
{
    public interface IEliminarDocsContratoPrestacionServiciosLN
    {
        Task<int> Eliminar(int id);
    }
}
