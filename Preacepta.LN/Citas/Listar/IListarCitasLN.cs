using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Citas.Listar
{
    public interface IListarCitasLN
    {
        Task<List<CitasDTO>> listar();
        Task<List<CitasDTO>> ListarPorIdCliente(int idCliente);
    }
}
