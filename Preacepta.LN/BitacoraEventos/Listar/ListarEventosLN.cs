
using Preacepta.AD.BitacoraEventos.Listar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.BitacoraEventos.Listar
{
    public class ListarEventosLN : IListarEventosLN
    {
        private readonly IListarEventosAD _listarEventosAD;
        public ListarEventosLN(IListarEventosAD listarEventosAD)
        {
            _listarEventosAD = listarEventosAD;
        }
        public async Task<List<BitacoraEventosDTO>> ListarTodos()
        {
            return await _listarEventosAD.ListarTodos();
        }
        public async Task<List<BitacoraEventosDTO>> ListarPorUsuario(string usuario)
        {
            if(string.IsNullOrEmpty(usuario)) return new List<BitacoraEventosDTO>();

            return await _listarEventosAD.ListarPorUsuario(usuario);
        }
    }
}
