using Preacepta.AD.Citas.Editar;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Citas.Editar
{
    public class EditarCitasAD : IEditarCitasAD
    {
        private readonly Contexto _contexto;
        public EditarCitasAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> editar(TCita editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                var existente = await _contexto.TCitas.FindAsync(editar.IdCita);
                if (existente == null)
                {
                    Console.WriteLine("No se encontró la cita para editar.");
                    return 0;
                }

                // Actualiza solo los campos necesarios
                existente.Fecha = editar.Fecha;
                existente.Hora = editar.Hora;
                existente.IdTipoCita = editar.IdTipoCita;
                existente.Anfitrion = editar.Anfitrion;
                existente.LinkVideo = editar.LinkVideo;

                int resultado = await _contexto.SaveChangesAsync();
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarCitasAD : {ex.Message}");
                return -1;
            }
        }


    }
}
