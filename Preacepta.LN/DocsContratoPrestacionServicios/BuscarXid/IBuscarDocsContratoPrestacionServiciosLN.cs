using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsContratoPrestacionServicios.BuscarXid
{
    public interface IBuscarDocsContratoPrestacionServiciosLN
    {
        Task<DocsContratoPrestacionServicioDTO?> buscar(int id);

    }
}
