using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsContratoPrestacionServicios.Listar
{
    public interface IListarDocsContratoPrestacionServiciosAD
    {
        Task<List<DocsContratoPrestacionServicioDTO>> listar();
    }
}
