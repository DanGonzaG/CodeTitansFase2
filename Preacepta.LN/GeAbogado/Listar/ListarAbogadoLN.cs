﻿using Preacepta.AD.CasosTipo.Listar;
using Preacepta.AD.GeAbogado.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeAbogado.Listar
{
    public class ListarAbogadoLN : IListarAbogadoLN
    {
        private readonly IListarAbogadoAD _listar;

        public ListarAbogadoLN(IListarAbogadoAD listar)
        {
            _listar = listar;
        }

        public async Task<List<GeAbogadoDTO>> listar()
        {
            List<GeAbogadoDTO> lista = await _listar.listar();
            return lista;
        }
    }
}
