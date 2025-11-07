using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.BitacoraEventos.Listar
{
    public interface IListarEventosLN
    {
        Task<List<BitacoraEventosDTO>> ListarTodos();
        Task<List<BitacoraEventosDTO>> ListarPorUsuario(string usuario);
    }
}
