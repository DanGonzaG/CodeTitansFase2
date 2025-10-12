using Preacepta.AD.BitacoraEventos.Crear;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.BitacoraEventos.Crear
{
    public class CrearEventosLN : ICrearEventosLN
    {
        private readonly ICrearEventosAD _crearEventosAD;
        public CrearEventosLN(ICrearEventosAD crearEventosAD)
        {
            _crearEventosAD = crearEventosAD;
            
        }
        public async Task <int> CrearEvento(TBitacoraEventos evento)
        {
            if (evento == null || string.IsNullOrEmpty(evento.Usuario))
                throw new ArgumentException("Evento Inválido");

            return await _crearEventosAD.InsertarEvento(evento);
        }
        public async Task RegistrarBitacoraAsync(string usuario, string tabla, string accion, int idRegistro, string? stackError = null)
        {
            var evento = new TBitacoraEventos
            {
                Usuario = usuario,
                Fecha_Hora = DateTime.Now,
                Tabla_Afectada = tabla,
                Accion = accion,
                Id_registro_afectado = idRegistro,
                Stack_error = stackError
            };
            await _crearEventosAD.InsertarEvento(evento);

        }
    }
}
