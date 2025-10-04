using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Citas.BuscarXid
{
    public interface IBuscarCitasLN
    {
        Task<CitasDTO?> buscar(int id);
        Task<List<CitasDTO>> obtenerTodas();
        Task<CitasDTO> ObtenerCitaConClientes(int idCita);
        Task<List<CitasDTO>> ListarPorIdCliente(int idCliente);
        Task<CitasDTO> CambiarEstadoYObtenerDatosAsync(int idCita, int nuevoEstado);
        Task<CitasDTO> ObtenerCitaConDocumentosAsync(int idCita);
    }
}
