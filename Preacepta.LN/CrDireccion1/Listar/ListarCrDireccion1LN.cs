using Preacepta.AD.CasosTipo.Listar;
using Preacepta.AD.CrDireccion1.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.CrDireccion1.Listar
{
    public class ListarCrDireccion1LN : IListarCrDireccion1LN
    {
        private readonly IListarCrDireccion1AD _listar;

        public ListarCrDireccion1LN(IListarCrDireccion1AD listar)
        {
            _listar = listar;
        }

        public async Task<List<CrProvinciaDTO>> listarProvincias()
        {
            List<CrProvinciaDTO> lista = await _listar.listarProvincias();
            return lista;
        }

        public async Task<List<CrCantonDTO>> listarCantones()
        {
            List<CrCantonDTO> lista = await _listar.listarCantones();
            return lista;
        }

        public async Task<List<CrDistritoDTO>> listarDistritos()
        {
            List<CrDistritoDTO> lista = await _listar.listarDistritos();
            return lista;
        }
    }
}
