using Preacepta.Modelos.AbstraccionesBD;
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
        Task<List<CitasDTO>> ListarPorFecha(DateOnly fecha);

        Task<List<CitasDTO>> ListarPorIdCliente(int idCliente);
        Task<List<CitasDTO>> TresCitasMasProximasXAfitrion(int id);
        Task<List<TCitasCliente>> TresCitasMasProximasXCliente(int idCliente);

        Task<CitasDTO?> ObtenerPorId(int idCita); 
        Task<GePersonaDTO?> ObtenerPersonaPorCedula(string cedula);
        Task<bool> ActualizarCita(CitasDTO cita);
        Task<List<CitasTipoDTO>> ListarTiposCita();
    }
}
