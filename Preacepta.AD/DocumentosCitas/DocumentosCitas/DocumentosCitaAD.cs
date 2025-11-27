using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Preacepta.AD.DocumentosCitas.DocumentosCitas
{
    public class DocumentosCitaAD : IDocumentosCitaAD
    {
        private readonly Contexto _contexto;

        public DocumentosCitaAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public List<TDocumentosCita> ObtenerPorCita(int idCita)
        {
            return _contexto.TDocumentosCita
                .Where(d => d.IdCita == idCita)
                .ToList();
        }

        public void Insertar(TDocumentosCita documento)
        {
            _contexto.TDocumentosCita.Add(documento);
            _contexto.SaveChanges();
        }

        public async Task<TDocumentosCita> ObtenerPorIdAsync(int idDocumento)
        {
            return await _contexto.TDocumentosCita
                .FirstOrDefaultAsync(d => d.Id == idDocumento);
        }


        public async Task GuardarCambiosAsync()
        {
            await _contexto.SaveChangesAsync();
        }

        public async Task ActualizarDescargarAsync(int idDocumento, bool permitirDescarga)
        {
            var documento = await _contexto.TDocumentosCita.FindAsync(idDocumento);
            if (documento != null)
            {
                documento.Descargar = permitirDescarga;
                _contexto.TDocumentosCita.Update(documento);
                await _contexto.SaveChangesAsync();
            }
        }

        public async Task ActualizarPermisoDescargaAsync(int idDocumento, bool permitirDescarga)
        {
            var documento = await _contexto.TDocumentosCita.FindAsync(idDocumento);
            if (documento == null)
                throw new Exception("Documento no encontrado");

            documento.Descargar = permitirDescarga;
            await _contexto.SaveChangesAsync(); 
        }

        /*public async Task<bool> EliminarAsync(TDocumentosCita documento)
        {
            if (documento == null) return false;

            // Remover de la base de datos
            _contexto.TDocumentosCita.Remove(documento);
            await _contexto.SaveChangesAsync();

            return true;
        }*/

        public async Task<bool> DeshabilitarAsync(int id)
        {
            var documento = await _contexto.TDocumentosCita.FindAsync(id);
            if (documento == null) return false;

            documento.Activo = false;
            _contexto.TDocumentosCita.Update(documento);
            await _contexto.SaveChangesAsync();

            return true;
        }

        public async Task RegistrarBitacoraAsync(TBitacoraEventos evento)
        {
            _contexto.TBitacoraEventos.Add(evento);
            await _contexto.SaveChangesAsync();
        }


        public async Task InsertarAsync(TDocumentosCita documento)
        {
            _contexto.TDocumentosCita.Add(documento);
            await _contexto.SaveChangesAsync();
        }

        public async Task ActualizarBatchAsync(List<TDocumentosCita> documentos)
        {
            foreach (var doc in documentos)
            {
                _contexto.Attach(doc);
                _contexto.Entry(doc).Property(x => x.Descargar).IsModified = true;
                _contexto.Entry(doc).Property(x => x.Activo).IsModified = true;
            }

            await _contexto.SaveChangesAsync();
        }


        public int InsertarDocumentoCifrado(TDocumentosCita doc)
        {
            _contexto.TDocumentosCita.Add(doc);
            _contexto.SaveChanges();

            return doc.Id; 
        }


        public void GuardarEncryptedKey(TDocumentoKey key)
        {
            _contexto.TDocumentoKey.Add(key);
            _contexto.SaveChanges();
        }

        public TDocumentoKey ObtenerEncryptedKey(int documentoId, string usuarioId)
        {
            return _contexto.TDocumentoKey
                .FirstOrDefault(k =>
                    k.DocumentoId == documentoId &&
                    k.UsuarioId == usuarioId &&
                    k.Activo);
        }

        public TDocumentoKey ObtenerEncryptedKeyPorDocumento(int documentoId, string usuarioId)
{
    return _contexto.TDocumentoKey
        .FirstOrDefault(x => x.DocumentoId == documentoId && x.UsuarioId == usuarioId && x.Activo);
}

        public async Task<TClavePublica> ObtenerClavePublicaUsuario(string usuarioId)
        {
            return await _contexto.TClavePublica
                .Where(k => k.UsuarioId == usuarioId && k.Activo)
                .OrderByDescending(k => k.FechaCreacion)
                .FirstOrDefaultAsync();
        }
        public async Task GuardarClavePublica(TClavePublica entidad)
        {
            _contexto.TClavePublica.Add(entidad);
            await _contexto.SaveChangesAsync();
        }
        public async Task<TClavePublica> GenerarClavePublicaUsuario(string usuarioId)
        {
            using var rsa = RSA.Create(2048);

            // Exportar clave pública en formato SPKI
            byte[] publicKeyBytes = rsa.ExportSubjectPublicKeyInfo();
            string publicKeyBase64 = Convert.ToBase64String(publicKeyBytes);

            var clave = new TClavePublica
            {
                UsuarioId = usuarioId,
                PublicKeyPem = publicKeyBase64,
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            };

            await GuardarClavePublica(clave);

            return clave;
        }


        public async Task<bool> ExisteCitaAsync(int idCita)
        {
            return await _contexto.TCitas.AnyAsync(c => c.IdCita == idCita);
        }

        public async Task InsertarDocumentoCifradoAsync(TDocumentosCita doc)
        {
            _contexto.TDocumentosCita.Add(doc);
            await _contexto.SaveChangesAsync(); 
        }

        public async Task GuardarEncryptedKeyAsync(TDocumentoKey key)
        {
            _contexto.TDocumentoKey.Add(key);
            await _contexto.SaveChangesAsync();
        }
       

        public async Task<List<string>> ObtenerUsuariosDeCitaAsync(int idCita, UserManager<IdentityUser> userManager)
        {
            var usuarios = new List<string>();


            var cita = await _contexto.TCitas
                .Include(c => c.TCitasClientes)
                .FirstOrDefaultAsync(c => c.IdCita == idCita);

            if (cita == null)
                return usuarios;

            var abogadoPersona = await _contexto.TGePersonas.FirstOrDefaultAsync(p => p.Cedula == cita.Anfitrion);

            if (abogadoPersona != null && !string.IsNullOrEmpty(abogadoPersona.Email))
            {
                var userAbogado = await userManager.FindByEmailAsync(abogadoPersona.Email);
                if (userAbogado != null)
                    usuarios.Add(userAbogado.Id);
            }

            foreach (var cc in cita.TCitasClientes)
            {
                var personaCliente = await _contexto.TGePersonas.FirstOrDefaultAsync(p => p.Cedula == cc.IdCliente);
                if (personaCliente != null && !string.IsNullOrEmpty(personaCliente.Email))
                {
                    var userCliente = await userManager.FindByEmailAsync(personaCliente.Email);
                    if (userCliente != null)
                        usuarios.Add(userCliente.Id);
                }
            }

            return usuarios.Distinct().ToList();
        }
        public bool ExisteClavePublica(string usuarioId)
        {
            return _contexto.TClavePublica.Any(x => x.UsuarioId == usuarioId && x.Activo);
        }


    }
}
