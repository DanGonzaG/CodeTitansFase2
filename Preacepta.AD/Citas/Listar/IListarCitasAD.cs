using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Citas.Listar
{
    public interface IListarCitasAD
    {
        Task<List<CitasDTO>> listar();
        Task<List<CitasDTO>> ListarPorIdCliente(int idCliente);
    }
}
