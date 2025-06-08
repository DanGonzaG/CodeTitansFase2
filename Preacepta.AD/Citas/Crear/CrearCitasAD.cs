using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Citas.Crear
{
    public class CrearCitasAD : ICrearCitasAD
    {
        private readonly Contexto _contexto;

        public CrearCitasAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TCita cita)
        {
            if (cita == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TCitas.AddAsync(cita);
                int guardado = await _contexto.SaveChangesAsync();
                Console.WriteLine($"Insertando cita: Fecha={cita.Fecha}, Hora={cita.Hora}, Tipo={cita.IdTipoCita}, Link={cita.LinkVideo}, Anfitrion={cita.Anfitrion}");
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearCitasAD {ex.Message}");
                return 0;
            }
        }
    }
}
