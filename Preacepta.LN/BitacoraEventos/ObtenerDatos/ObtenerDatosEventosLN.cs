using Preacepta.AD.BitacoraEventos.BuscarXid;
using Preacepta.AD.BitacoraEventos.Listar;
using Preacepta.LN.BitacoraEventos.Listar;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.BitacoraEventos.ObtenerDatos
{
    public class ObtenerDatosEventosLN : IObtenerDatosEventosLN
    {
        private readonly IListarEventosAD _listarEventosAD;
        private readonly IBuscarEventosAD _buscarEventosAD;

        public ObtenerDatosEventosLN(IListarEventosAD listarEventosAD, IBuscarEventosAD buscarEventosAD)
        {
            _listarEventosAD = listarEventosAD ?? throw new ArgumentNullException(nameof(listarEventosAD));
            _buscarEventosAD = buscarEventosAD ?? throw new ArgumentNullException(nameof(buscarEventosAD));
        }

        public async Task <List<BitacoraEventosDTO>> ListarTodos()
        {
            return await _listarEventosAD.ListarTodos();
        }

        public async Task<List<BitacoraEventosDTO>> ListarPorUsuario(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario)) return new List<BitacoraEventosDTO>();
            return await _listarEventosAD.ListarPorUsuario(usuario);
        }

        public async Task<List<BitacoraEventosDTO>> ObtenerEventosPorTabla(string tablaAfectada)
        {
            if (string.IsNullOrEmpty(tablaAfectada)) return new List<BitacoraEventosDTO>();
            var todos = await _listarEventosAD.ListarTodos();
            return todos.Where(e => e.Tabla_Afectada.Equals(tablaAfectada, StringComparison.OrdinalIgnoreCase)).ToList(); 
        }
        public async Task<BitacoraEventosDTO> ObtenerXid(int Id_evento)
        {
            var evento = await _buscarEventosAD.BuscarXid(Id_evento);
            if (evento == null) return null;

            return new BitacoraEventosDTO
            { 
                Id_evento = evento.Id_evento,
                Usuario = evento.Usuario,
                Fecha_Hora = evento.Fecha_Hora,
                Accion = evento.Accion,
                Tabla_Afectada = evento.Tabla_Afectada,
                Id_registro_afectado = evento.Id_registro_afectado,
                Stack_error = evento.Stack_error
               
            };
        }
        public Task<List<BitacoraEventosDTO>> ListarPorRangoFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            return _listarEventosAD.ListarPorRangoFecha(fechaInicio, fechaFin);
        }

    }
}
 