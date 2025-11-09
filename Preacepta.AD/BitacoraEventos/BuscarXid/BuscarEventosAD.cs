using Microsoft.EntityFrameworkCore;
using Preacepta.AD.Citas.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.BitacoraEventos.BuscarXid
{
    public class BuscarEventosAD : IBuscarEventosAD
    {
        private readonly Contexto _contexto;
        public BuscarEventosAD(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<TBitacoraEventos?> BuscarXid(int id_evento)
        {
            try
            {
                return await _contexto.TBitacoraEventos
                    .FirstOrDefaultAsync(e => e.Id_evento == id_evento);
            }catch (Exception ex) {
                Console.WriteLine($"Error en BuscarPorId: {ex.Message}");
                return null;
        }
    }
        public async Task <List<TBitacoraEventos>> ObtenerTodas()
        {
            try
            {
                return await _contexto.TBitacoraEventos
                    .OrderByDescending(e => e.Fecha_Hora)
                    .ToListAsync();
            }catch (Exception ex) {
                Console.WriteLine($"Error en ObtenerTodas: {ex.Message}");
                return new List<TBitacoraEventos> ();
        }
    }
        public async Task<List<BitacoraEventosDTO>> ObtenerPorUsuario(string usuario)
        {
            try
            {
                return await _contexto.TBitacoraEventos
                    .Where(e => e.Usuario == usuario)
                    .OrderByDescending(e => e.Fecha_Hora)
                    .Select(e => new BitacoraEventosDTO
                    {
                        Usuario = e.Usuario,
                        Fecha_Hora = e.Fecha_Hora,
                        Accion = e.Accion,
                        Tabla_Afectada = e.Tabla_Afectada,
                        Id_registro_afectado = e.Id_registro_afectado,
                        Stack_error = e.Stack_error
                }).ToListAsync();
            }catch(Exception ex) {
                Console.WriteLine($"Error en ObtenerPorUsuario: {ex.Message}");
                return new List<BitacoraEventosDTO> ();
        }
   }
    }
}