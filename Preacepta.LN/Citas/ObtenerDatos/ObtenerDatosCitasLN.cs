﻿using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Citas.ObtenerDatos

{
    public class ObtenerDatosCitasLN : IObtenerDatosCitasLN
    {
        private readonly Contexto _contexto;
        public ObtenerDatosCitasLN(Contexto contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }
        public CitasDTO ObtenerDeDB(TCita baseDatos)
        {
            return new CitasDTO
            {
                IdCita = baseDatos.IdCita,
                Fecha = baseDatos.Fecha,
                Hora = baseDatos.Hora,
                IdTipoCita = baseDatos.IdTipoCita,
                LinkVideo = baseDatos.LinkVideo,
                Anfitrion = baseDatos.Anfitrion,
                NombreTipoCita = baseDatos.IdTipoCitaNavigation?.Nombre,
                NombreAnfitrion = $"{baseDatos.AnfitrionNavigation?.CedulaNavigation?.Nombre} {baseDatos.AnfitrionNavigation?.CedulaNavigation?.Apellido1} {baseDatos.AnfitrionNavigation?.CedulaNavigation?.Apellido2}"
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TCita ObtenerDeFront(CitasDTO Formulario)
        {
            return new TCita
            {
                IdCita = Formulario.IdCita,
                Fecha = Formulario.Fecha,
                Hora = Formulario.Hora,
                IdTipoCita = Formulario.IdTipoCita,
                LinkVideo = Formulario.LinkVideo,
                Anfitrion = Formulario.Anfitrion,
            };
        }
        public async Task<List<CitasDTO>> ListarCitasAsync()
        {
            var citasDb = await _contexto.TCitas
                .Include(c => c.IdTipoCitaNavigation) // si quieres incluir la navegación para NombreTipoCita
                .ToListAsync();

            return citasDb.Select(c => ObtenerDeDB(c)).ToList();
        }

        public async Task<List<CitasDTO>> ListarCitasPorClienteAsync(int idCliente)
        {
            var citas = await _contexto.TCitas
                .Include(c => c.IdTipoCitaNavigation)
                .Include(c => c.AnfitrionNavigation).ThenInclude(a => a.CedulaNavigation)
                .Include(c => c.TCitasClientes)
                .Where(c => c.TCitasClientes.Any(cc => cc.IdCliente == idCliente))
                .ToListAsync();

            return citas.Select(c => ObtenerDeDB(c)).ToList();
        }
        public async Task<List<CitasDTO>> ListarAnfitrionAsync()
        {
            var citasDb = await _contexto.TCitas
                .Include(c => c.AnfitrionNavigation) // si quieres incluir la navegación para Anfitrion
                .ToListAsync();

            return citasDb.Select(c => ObtenerDeDB(c)).ToList();
        }

        public async Task<CitasDTO> ObtenerCitaConDocumentosAsync(int idCita)
        {
            var cita = await _contexto.TCitas
                .Include(c => c.IdTipoCitaNavigation)
                .Include(c => c.AnfitrionNavigation).ThenInclude(a => a.CedulaNavigation)
                .FirstOrDefaultAsync(c => c.IdCita == idCita);

            if (cita == null) return null;

            var dto = ObtenerDeDB(cita);

            var documentos = await _contexto.TDocumentosCita
                .Where(d => d.IdCita == idCita)
                .ToListAsync();

            dto.Documentos = documentos.Select(d => new DocumentosCitaDTO
            {
                Id = d.Id,
                IdCita = d.IdCita,
                NombreArchivo = d.NombreArchivo,
                RutaArchivo = d.RutaArchivo,
                FechaSubida = d.FechaSubida,
                Descargar = d.Descargar
            }).ToList();

            return dto;
        }
    }
}
