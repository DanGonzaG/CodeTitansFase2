using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.BitacoraEventos.Crear
{
    public class CrearEventosAD : ICrearEventosAD
    {
        private readonly Contexto _contexto;
        public CrearEventosAD(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task <int> InsertarEvento(TBitacoraEventos evento)
        {
            try
            {
                if (evento.Fecha_Hora == default)
                    evento.Fecha_Hora = DateTime.Now;
                _contexto.TBitacoraEventos.Add(evento);
                await _contexto.SaveChangesAsync();

                return evento.Id_evento;
            }catch (Exception ex) {
                Console.WriteLine($"Error al insertar evento en bitacora: {ex.Message}");
                return -1;
            }
            }
    }
}
