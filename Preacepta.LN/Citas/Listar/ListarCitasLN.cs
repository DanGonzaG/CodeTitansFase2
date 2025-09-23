
using Preacepta.AD.Citas.Listar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Citas.Listar

{
    public class ListarCitasLN : IListarCitasLN
    {
        private readonly IListarCitasAD _listar;

        public ListarCitasLN(IListarCitasAD listar)
        {
            _listar = listar;
        }

        public async Task<List<CitasDTO>> listar()
        {
            List<CitasDTO> lista = await _listar.listar();
            return lista;
        }
        public async Task<List<CitasDTO>> ListarPorIdCliente(int idCliente)
        {
            return await _listar.ListarPorIdCliente(idCliente); 
        }

        public async Task<List<CitasDTO>> TresCitasMasProximasXAfitrion(int idCliente)
        {            
            return await _listar.TresCitasMasProximasXAfitrion(idCliente);
        }

        public async Task<List<TCitasCliente>> TresCitasMasProximasXCliente(int idCliente)
        {
            return await _listar.TresCitasMasProximasXCliente(idCliente);
        }
        public async Task<List<CitasDTO>> ListarPorFecha(DateTime fecha)
        {
            
            DateOnly fechaOnly = DateOnly.FromDateTime(fecha);

            
            return await _listar.ListarPorFecha(fechaOnly);
        }
        public async Task<CitasDTO> ObtenerPorId(int id)
        {
            return await _listar.ObtenerPorId(id);
        }

        public async Task<GePersonaDTO> ObtenerPersonaPorCedula(string cedula)
        {
            return await _listar.ObtenerPersonaPorCedula(cedula);
        }

        public async Task<List<CitasTipoDTO>> ListarTiposCita()
        {
            return await _listar.ListarTiposCita();
        }

        public async Task<List<CitasDTO>> ListarPorFecha(DateOnly fecha)
        {
            return await _listar.ListarPorFecha(fecha); 
        }
    }
}