using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Citas.Listar
{
    public class ListarCitasAD : IListarCitasAD
    {
        private readonly Contexto _contexto;

        public ListarCitasAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<CitasDTO>> listar()
        {
            try
            {
                return await _contexto.TCitas
                    .Include(c => c.IdTipoCitaNavigation)
                    .Include(c => c.AnfitrionNavigation)
                    .ThenInclude(a => a.CedulaNavigation)
                    .Select(cita => new CitasDTO
                {
                    IdCita = cita.IdCita,
                    Fecha = cita.Fecha,
                    Hora = cita.Hora,
                    IdTipoCita = cita.IdTipoCita,
                    Anfitrion = cita.Anfitrion,
                    LinkVideo = cita.LinkVideo,
                    AnfitrionNavigation = cita.AnfitrionNavigation,
                    IdTipoCitaNavigation = cita.IdTipoCitaNavigation,
                        NombreTipoCita = cita.IdTipoCitaNavigation != null
                     ? cita.IdTipoCitaNavigation.Nombre
                     : null

                        /*Anfitrion = cita.AnfitrionNavigation != null ? cita.AnfitrionNavigation.Cedula : null,
                            LinkVideo = cita.LinkVideo,
                        NombreTipoCita = cita.IdTipoCitaNavigation.Nombre*/

                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<CitasDTO>();
            }

        }
    }
}
