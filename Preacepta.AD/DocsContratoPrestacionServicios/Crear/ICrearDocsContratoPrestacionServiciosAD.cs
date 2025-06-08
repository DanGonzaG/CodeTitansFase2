using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsContratoPrestacionServicios.Crear
{
    public interface ICrearDocsContratoPrestacionServiciosAD
    {
        Task<int> crear(TDocsContratoPrestacionServicio crear);
    }
}
