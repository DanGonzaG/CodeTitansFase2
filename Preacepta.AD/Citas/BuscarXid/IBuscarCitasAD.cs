using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Citas.BuscarXid
{
    public interface IBuscarCitasAD
    {
        Task<TCita?> buscar(int id);
        Task<List<TCita>> obtenerTodas();
        Task<CitasDTO> ObtenerCitaConClientes(int idCita);

        Task<List<TCita>> ListarPorIdCliente(int idCliente);
        Task<TCita?> TerminarCitaYObtenerDatosAsync(int idCita);

    }
}
