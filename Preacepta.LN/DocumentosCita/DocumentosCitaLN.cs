using Microsoft.AspNetCore.Http;
using Preacepta.AD.DocumentosCitas.DocumentosCitas;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocumentosCita
{
    public class DocumentosCitaLN : IDocumentosCitaLN
    {
        private readonly IDocumentosCitaAD _documentosAD;

        public DocumentosCitaLN(IDocumentosCitaAD documentosAD)
        {
            _documentosAD = documentosAD;
        }

        public List<DocumentosCitaDTO> ObtenerPorCita(int idCita)
        {
            var documentos = _documentosAD.ObtenerPorCita(idCita);
            return documentos.Select(d => new DocumentosCitaDTO
            {
                Id = d.Id,
                IdCita = d.IdCita,
                NombreArchivo = d.NombreArchivo,
                RutaArchivo = d.RutaArchivo,
                FechaSubida = d.FechaSubida
            }).ToList();
        }

        public void SubirArchivo(int idCita, IFormFile archivo)
        {
            var nombre = Path.GetFileName(archivo.FileName);
            var carpetaDocumentos = Path.Combine("wwwroot", "documentos");

            // Crear la carpeta si no existe
            if (!Directory.Exists(carpetaDocumentos))
            {
                Directory.CreateDirectory(carpetaDocumentos);
            }

            var rutaLocal = Path.Combine(carpetaDocumentos, nombre);

            using (var stream = new FileStream(rutaLocal, FileMode.Create))
            {
                archivo.CopyTo(stream);
            }

            var entidad = new TDocumentosCita
            {
                IdCita = idCita,
                NombreArchivo = nombre,
                RutaArchivo = "/documentos/" + nombre,
                FechaSubida = DateTime.Now
            };

            _documentosAD.Insertar(entidad);
        }
        public async Task ActualizarPermisoDescargaAsync(int idDocumento, bool permitirDescarga)
        {
            var documento = await _documentosAD.ObtenerPorIdAsync(idDocumento);
            if (documento == null)
                throw new Exception("Documento no encontrado");

            documento.Descargar = permitirDescarga;
            await _documentosAD.GuardarCambiosAsync();
        }

        public async Task<DocumentosCitaDTO> ObtenerPorIdAsync(int idDocumento)
        {
            var entidad = await _documentosAD.ObtenerPorIdAsync(idDocumento);
            if (entidad == null) return null;

            return new DocumentosCitaDTO
            {
                Id = entidad.Id,
                IdCita = entidad.IdCita,
                NombreArchivo = entidad.NombreArchivo,
                RutaArchivo = entidad.RutaArchivo,
                FechaSubida = entidad.FechaSubida,
                Descargar = entidad.Descargar
            };
        }
    }

}
