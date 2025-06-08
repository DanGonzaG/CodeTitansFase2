using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsContratoPrestacionServicios.Editar
{
    public interface IEditarDocsContratoPrestacionServiciosAD
    {
        Task<int> Editar(TDocsContratoPrestacionServicio editar);
    }
}
