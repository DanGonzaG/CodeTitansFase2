using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.BitacoraEventos.Listar
{
    public class ListarEventosAD : IListarEventosAD
    {
        private readonly Contexto _contexto;
        public ListarEventosAD(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<List<BitacoraEventosDTO>> ListarTodos()
        {
            return await _contexto.TBitacoraEventos
               .OrderByDescending(e => e.Fecha_Hora)
               .Select(e => new BitacoraEventosDTO
               {
                   Id_evento = e.Id_evento,
                   Usuario = e.Usuario,
                   Fecha_Hora = e.Fecha_Hora,
                   Accion = e.Accion,
                   Tabla_Afectada = e.Tabla_Afectada,
                   Id_registro_afectado= e.Id_registro_afectado,
                   Stack_error = e.Stack_error
               }).ToListAsync();

        }
        public async Task <List<BitacoraEventosDTO>>ListarPorUsuario(string usuario)
        {
            return await _contexto.TBitacoraEventos
                .Where(e => e.Usuario ==usuario)
                .OrderByDescending (e => e.Fecha_Hora)
                .Select(e => new BitacoraEventosDTO
                {
                    Id_evento = e.Id_evento,
                    Usuario= e.Usuario,
                    Fecha_Hora= e.Fecha_Hora,
                    Accion = e.Accion,
                    Tabla_Afectada= e.Tabla_Afectada,
                    Id_registro_afectado = e.Id_registro_afectado,
                    Stack_error = e.Stack_error
                }).ToListAsync ();

        }
    }
}
