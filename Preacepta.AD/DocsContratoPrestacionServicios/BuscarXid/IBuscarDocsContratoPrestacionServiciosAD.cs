using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsContratoPrestacionServicios.BuscarXid
{
    public interface IBuscarDocsContratoPrestacionServiciosAD
    {
        Task<TDocsContratoPrestacionServicio?> buscar(int id);
    }
}