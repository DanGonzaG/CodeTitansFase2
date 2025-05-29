using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.GeRedesSociales.Listar
{
    public interface IListarRedesSocialesLN
    {
        Task<List<GeRedesSocialeDTO>> listar();
    }
}
