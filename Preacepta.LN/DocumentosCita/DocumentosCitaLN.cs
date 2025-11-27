using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.AD.DocumentosCitas.DocumentosCitas;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
                FechaSubida = d.FechaSubida,
                Descargar = d.Descargar,
                Activo = d.Activo,
            }).ToList();
        }

        public void SubirArchivo(int idCita, IFormFile archivo)
        {
            var nombre = Path.GetFileName(archivo.FileName);
            var carpetaDocumentos = Path.Combine("wwwroot", "documentos");

           
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
                FechaSubida = DateTime.Now,
                Descargar = false
            };

            _documentosAD.Insertar(entidad);
        }

        public async Task ActualizarPermisoDescargaAsync(int idDocumento, bool permitirDescarga)
        {
            var documento = await _documentosAD.ObtenerPorIdAsync(idDocumento);
            if (documento == null)
                throw new Exception("Documento no encontrado");

            documento.Descargar = permitirDescarga;

           
            await _documentosAD.ActualizarDescargarAsync(idDocumento, permitirDescarga);
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
                Descargar = entidad.Descargar,
                Activo = entidad.Activo,
                Algoritmo = entidad.Algoritmo,
                ContentType = entidad.ContentType,
                IV = entidad.IV,
                OwnerId = entidad.OwnerId,
                ArchivoCifrado = entidad.ArchivoCifrado,
            };
        }
       

        public async Task<bool> DeshabilitarAsync(int id)
        {
            return await _documentosAD.DeshabilitarAsync(id);
        }

        
        public async Task<TDocumentosCita> SubirDocumentoAsync(int idCita, string nombreArchivo, string rutaArchivo)
        {
            var documento = new TDocumentosCita
            {
                IdCita = idCita,
                NombreArchivo = nombreArchivo,
                RutaArchivo = rutaArchivo,
                FechaSubida = DateTime.Now,
                Descargar = false
            };

             _documentosAD.Insertar(documento);
            return documento;
        }

        public async Task ActualizarBatchAsync(List<DocumentosCitaDTO> documentos)
        {
            var entidades = new List<TDocumentosCita>();

            foreach (var dto in documentos)
            {
                var doc = await _documentosAD.ObtenerPorIdAsync(dto.Id);
                if (doc != null)
                {
                    doc.Descargar = dto.Descargar;
                    doc.Activo = dto.Activo;
                    entidades.Add(doc);
                }
            }

            await _documentosAD.ActualizarBatchAsync(entidades);
        }

        public async Task<TClavePublica> ObtenerClavePublicaUsuario(string usuarioId)
        {
            return await _documentosAD.ObtenerClavePublicaUsuario(usuarioId);
        }

        public async Task<TClavePublica> GenerarClavePublicaUsuario(string usuarioId)
        {
            using var rsa = RSA.Create(2048);

            string publicKeyPEM = ExportarClavePublicaPEM(rsa);

            var clave = new TClavePublica
            {
                UsuarioId = usuarioId,
                PublicKeyPem = publicKeyPEM,
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            };

            await _documentosAD.GuardarClavePublica(clave);

            return clave;
        }


        private string ExportarClavePublicaPEM(RSA rsa)
    {
        byte[] spki = rsa.ExportSubjectPublicKeyInfo();

        string base64 = Convert.ToBase64String(spki);

        var sb = new StringBuilder();
        sb.AppendLine("-----BEGIN PUBLIC KEY-----");

        const int LINE_LENGTH = 64;
        for (int i = 0; i < base64.Length; i += LINE_LENGTH)
            sb.AppendLine(base64.Substring(i, Math.Min(LINE_LENGTH, base64.Length - i)));

        sb.AppendLine("-----END PUBLIC KEY-----");

        return sb.ToString();
    }

        public async Task<TDocumentoKey> ObtenerKeyDocumentoUsuario(int documentoId, string usuarioId)
{
    return _documentosAD.ObtenerEncryptedKeyPorDocumento(documentoId, usuarioId);
}

        public async Task GuardarClavePublicaUsuario(string usuarioId, string publicKeyBase64)
        {
            var entidad = new TClavePublica
            {
                UsuarioId = usuarioId,
                PublicKeyPem = publicKeyBase64,
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            };

            await _documentosAD.GuardarClavePublica(entidad);
        }

        public bool ExisteClavePublica(string usuarioId)
        {
            return _documentosAD.ExisteClavePublica(usuarioId);
        }


    }
}

