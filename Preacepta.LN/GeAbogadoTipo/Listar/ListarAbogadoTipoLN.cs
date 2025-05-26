using Preacepta.AD.GeAbogadoTipo.Listar;
using Preacepta.AD.GePersona.Listar;
using Preacepta.LN.GePersona.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeAbogadoTipo.Listar
{
    public class ListarAbogadoTipoLN : IListarAbogadoTipoLN
    {
       
            private readonly IListarAbogadoTipoAD _listar;

            public ListarAbogadoTipoLN(IListarAbogadoTipoAD listar)
            {
                _listar = listar;
            }

            public async Task<List<GeAbogadoTipoDTO>> listar()
            {
                List<GeAbogadoTipoDTO> lista = await _listar.listar();
                return lista;
            }
        }

    }

