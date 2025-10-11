using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.BitacoraEventos.ObtenerDatos
{
   public interface IObtenerDatosEventosLN
    {
        Task<List<BitacoraEventosDTO>> ObtenerEventosPorTabla(string tablaAfectada);
        Task<List<BitacoraEventosDTO>> ListarTodos();
        Task<List<BitacoraEventosDTO>> ListarPorUsuario(string usuario);
        Task<BitacoraEventosDTO?> ObtenerXid(int id_evento);



    }
}
