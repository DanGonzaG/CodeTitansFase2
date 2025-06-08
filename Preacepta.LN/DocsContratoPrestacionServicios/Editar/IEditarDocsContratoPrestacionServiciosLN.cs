using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsContratoPrestacionServicios.Editar
{
    public interface IEditarDocsContratoPrestacionServiciosLN
    {
        Task<int> Editar(DocsContratoPrestacionServicioDTO editar);
    }
}
