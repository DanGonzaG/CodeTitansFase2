using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GePersona.BuscarXid
{
    public class BuscarXidGePersonaAD : IBuscarXidGePersonaAD
    {
        private readonly Contexto _contexto;

        public BuscarXidGePersonaAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TGePersona?> buscar(int id)
        {
            try
            {
                var tGePersona = await _contexto.TGePersonas
                    .Include(t => t.Direccion1Navigation)
                    .FirstOrDefaultAsync(m => m.Cedula == id);
                return tGePersona;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarXidGePersonaAD, no se encontro id: {ex.Message}");
                return null;
            }

        }
    }
}
