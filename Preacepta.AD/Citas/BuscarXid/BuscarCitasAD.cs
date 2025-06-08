using Microsoft.EntityFrameworkCore;
using Preacepta.AD.Citas.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Citas.BuscarXid

{
    public class BuscarCitasAD : IBuscarCitasAD
    {
        private readonly Contexto _contexto;

        public BuscarCitasAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TCita?> buscar(int id)
        {
            try
            {
                var cita = await _contexto.TCitas
                    .Include(c => c.IdTipoCitaNavigation)
                    .FirstOrDefaultAsync(c => c.IdCita == id);

                return cita;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCitasAD, no se encontro id: {ex.Message}");
                return null;
            }
        }

    }
}
