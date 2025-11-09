using Microsoft.EntityFrameworkCore;
using Preacepta.AD.Citas.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
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
                    .Include(c => c.AnfitrionNavigation)
                        .ThenInclude(u => u.CedulaNavigation)
                    .FirstOrDefaultAsync(c => c.IdCita == id);

                return cita;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCitasAD, no se encontro id: {ex.Message}");
                return null;
            }
        }
        public async Task<List<TCita>> obtenerTodas()
        {
            try
            {
                var citas = await _contexto.TCitas
                    .Include(c => c.IdTipoCitaNavigation)  
                    .ToListAsync();
                Console.WriteLine($"Citas obtenidas de BD: {citas.Count}");
                return citas;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCitasAD.obtenerTodas: {ex.Message}");
                return new List<TCita>();
            }
        }

        public async Task<CitasDTO> ObtenerCitaConClientes(int idCita)
        {
            var cita = await _contexto.TCitas
        .Include(c => c.TCitasClientes)
            .ThenInclude(tc => tc.IdClienteNavigation)
        .Include(c => c.AnfitrionNavigation)
            .ThenInclude(a => a.CedulaNavigation)
        .Include(c => c.IdTipoCitaNavigation)
        .FirstOrDefaultAsync(c => c.IdCita == idCita);

            if (cita == null) return null;

            var citaDTO = new CitasDTO
            {
                IdCita = cita.IdCita,
                Fecha = cita.Fecha,
                Hora = cita.Hora,
                IdTipoCita = cita.IdTipoCita,
                Anfitrion = cita.Anfitrion,
                LinkVideo = cita.LinkVideo,
                Estado = cita.Estado,
                NombresClientes = cita.TCitasClientes
                    .Select(cc => cc.IdClienteNavigation.Nombre + " " +
                                  cc.IdClienteNavigation.Apellido1 + " " +
                                  cc.IdClienteNavigation.Apellido2)
                    .ToList(),
                Documentos = cita.DocumentosCita
            .Select(d => new DocumentosCitaDTO
            {
                Id = d.Id,
                IdCita = d.IdCita,
                NombreArchivo = d.NombreArchivo,
                RutaArchivo = d.RutaArchivo,
                FechaSubida = d.FechaSubida,
                Descargar = d.Descargar
            }).ToList()
            };
            return citaDTO;
        }
        public async Task<List<TCita>> ListarPorIdCliente(int idCliente)
        {
            var citas = await _contexto.TCitasClientes
                .Where(cc => cc.IdCliente == idCliente)
                .Select(cc => cc.IdCitaNavigation)
                .Include(c => c.IdTipoCitaNavigation)
                .ToListAsync();

            return citas;
        }

        public async Task<TCita?> CambiarEstadoYObtenerDatosAsync(int idCita, int nuevoEstado)
        {
            var cita = await _contexto.TCitas
                .Include(c => c.TCitasClientes)
                .ThenInclude(tc => tc.IdClienteNavigation)
                .FirstOrDefaultAsync(c => c.IdCita == idCita);

            if (cita == null) return null;

            cita.Estado = nuevoEstado;
            await _contexto.SaveChangesAsync();

            return cita;
        }

    }
}

  