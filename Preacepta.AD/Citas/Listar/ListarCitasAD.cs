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
                    Estado = cita.Estado,
                    AnfitrionNavigation = cita.AnfitrionNavigation,
                    IdTipoCitaNavigation = cita.IdTipoCitaNavigation,
                        NombreTipoCita = cita.IdTipoCitaNavigation != null
                     ? cita.IdTipoCitaNavigation.Nombre
                     : null

                        

                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<CitasDTO>();
            }

        }
        public async Task<List<CitasDTO>> ListarPorIdCliente(int idCliente)
        {
            try
            {
                var citas = await _contexto.TCitasClientes
    .Where(cc => cc.IdCliente == idCliente)
    .Include(cc => cc.IdCitaNavigation) // La cita
        .ThenInclude(c => c.IdTipoCitaNavigation) // Tipo de cita
    .Include(cc => cc.IdCitaNavigation.TCitasClientes) // Clientes de la cita
        .ThenInclude(tc => tc.IdClienteNavigation) // Info del cliente
    .Include(cc => cc.IdCitaNavigation.AnfitrionNavigation) // Anfitrión
        .ThenInclude(a => a.CedulaNavigation)
    .ToListAsync();

                var citasDTO = citas.Select(cc => new CitasDTO
                {
                        IdCita = cc.IdCitaNavigation.IdCita,
                        Fecha = cc.IdCitaNavigation.Fecha,
                        Hora = cc.IdCitaNavigation.Hora,
                        IdTipoCita = cc.IdCitaNavigation.IdTipoCita,
                        LinkVideo = cc.IdCitaNavigation.LinkVideo,
                        Anfitrion = cc.IdCitaNavigation.Anfitrion,
                        Estado = cc.IdCitaNavigation.Estado,
                        NombreTipoCita = cc.IdCitaNavigation.IdTipoCitaNavigation != null
                                         ? cc.IdCitaNavigation.IdTipoCitaNavigation.Nombre
                                         : null,
                        TCitasClientes = cc.IdCitaNavigation.TCitasClientes
                            .Select(c => new TCitasCliente
                            {
                                IdCiCliente = c.IdCiCliente,
                                IdCita = c.IdCita,
                                IdCliente = c.IdCliente,
                                IdClienteNavigation = c.IdClienteNavigation
                            }).ToList()
                    }
                ).ToList();

                return citasDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar citas por cliente: {ex.Message}");
                return new List<CitasDTO>();
            }
        }


        public async Task<List<CitasDTO>> TresCitasMasProximasXAfitrion (int id) 
        {
            return await _contexto.TCitas
                .Where(cedula => cedula.Anfitrion == id && cedula.Fecha >= DateOnly.FromDateTime(DateTime.Today))
                .OrderByDescending(fecha => fecha.Fecha)
                .Take(3)
                .Select(a => new CitasDTO 
                {
                    IdCita = a.IdCita,
                    Fecha = a.Fecha,
                })
                .ToListAsync();
                
        }

        public async Task<List<TCitasCliente>> TresCitasMasProximasXCliente(int idCliente)
        {
            var hoy = DateOnly.FromDateTime(DateTime.Today);

            var lista = await (from c in _contexto.TCitasClientes
                               join tc in _contexto.TCitas on c.IdCita equals tc.IdCita
                               where c.IdCliente == idCliente && tc.Fecha >= hoy
                               orderby tc.Fecha
                               select new TCitasCliente
                               {
                                   IdCita = c.IdCita,
                                   IdCitaNavigation = c.IdCitaNavigation,
                               }).Take(3).ToListAsync();

            return lista;
        }

        public async Task<List<CitasDTO>> ListarPorFecha(DateOnly fecha)
        {
            return await (
                from c in _contexto.TCitas
                join t in _contexto.TCitasTipos on c.IdTipoCita equals t.Id
                where c.Fecha == fecha
                select new CitasDTO
                {
                    IdCita = c.IdCita,
                    Hora = c.Hora,
                    NombreTipoCita = t.Nombre
                }).ToListAsync();
        }

        public async Task<CitasDTO> ObtenerPorId(int id)
        {
            return await _contexto.TCitas
                .Include(c => c.IdTipoCitaNavigation)
                .Include(c => c.AnfitrionNavigation)
                    .ThenInclude(a => a.CedulaNavigation)
                .Include(c => c.TCitasClientes) 
                    .ThenInclude(tc => tc.IdClienteNavigation) 
                .Where(c => c.IdCita == id)
                .Select(c => new CitasDTO
                {
                    IdCita = c.IdCita,
                    Fecha = c.Fecha,
                    Hora = c.Hora,
                    IdTipoCita = c.IdTipoCita,
                    LinkVideo = c.LinkVideo,
                    Anfitrion = c.Anfitrion,
                    Estado = c.Estado,
                    NombreTipoCita = c.IdTipoCitaNavigation != null ? c.IdTipoCitaNavigation.Nombre : null,
                    NombreAnfitrion = c.AnfitrionNavigation != null
                        ? $"{c.AnfitrionNavigation.CedulaNavigation.Nombre} {c.AnfitrionNavigation.CedulaNavigation.Apellido1} {c.AnfitrionNavigation.CedulaNavigation.Apellido2}"
                        : null,
                   
                    TCitasClientes = c.TCitasClientes.Select(tc => new TCitasCliente
                    {
                        IdCiCliente = tc.IdCiCliente,
                        IdCita = tc.IdCita,
                        IdCliente = tc.IdCliente,
                        IdClienteNavigation = tc.IdClienteNavigation
                    }).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task<GePersonaDTO?> ObtenerPersonaPorCedula(string cedula)
        {
            if (!int.TryParse(cedula, out int cedulaInt))
                return null;

            return await _contexto.TGePersonas
                .Where(p => p.Cedula == cedulaInt) 
                .Select(p => new GePersonaDTO
                {
                    Cedula = p.Cedula, 
                    Nombre = p.Nombre,
                    Apellido1 = p.Apellido1,
                    Apellido2 = p.Apellido2
                })
                .FirstOrDefaultAsync();
        }


        public async Task<List<CitasTipoDTO>> ListarTiposCita()
        {
            return await _contexto.TCitasTipos
                .Select(t => new CitasTipoDTO
                {
                    Id = t.Id,
                    Nombre = t.Nombre
                }).ToListAsync();
        }

        public async Task<bool> ActualizarCita(CitasDTO citaDTO)
        {
            var cita = await _contexto.TCitas.FindAsync(citaDTO.IdCita);
            if (cita != null)
            {
                cita.Fecha = citaDTO.Fecha;
                cita.Hora = citaDTO.Hora;
                cita.IdTipoCita = citaDTO.IdTipoCita;
                cita.LinkVideo = citaDTO.LinkVideo;
                cita.Anfitrion = citaDTO.Anfitrion;
                cita.Estado = citaDTO.Estado;

                await _contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}