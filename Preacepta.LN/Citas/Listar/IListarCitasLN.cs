using Preacepta.Modelos.AbstraccionesBD;
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
        Task<List<CitasDTO>> ListarPorFecha(DateOnly fecha);

        Task<List<CitasDTO>> ListarPorIdCliente(int idCliente);
        Task<List<CitasDTO>> TresCitasMasProximasXAfitrion(int idCliente);
        Task<List<TCitasCliente>> TresCitasMasProximasXCliente(int idCliente);

        Task<CitasDTO> ObtenerPorId(int idCita);
        Task<GePersonaDTO> ObtenerPersonaPorCedula(string cedula);
        Task<List<CitasTipoDTO>> ListarTiposCita();
    }
}
