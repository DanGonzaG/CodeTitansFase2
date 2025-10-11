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
    }
}
