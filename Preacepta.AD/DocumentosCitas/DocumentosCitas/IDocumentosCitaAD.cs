using Microsoft.AspNetCore.Identity;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocumentosCitas.DocumentosCitas
{
    public interface IDocumentosCitaAD
    {
        List<TDocumentosCita> ObtenerPorCita(int idCita);
        void Insertar(TDocumentosCita documento);
        Task<TDocumentosCita> ObtenerPorIdAsync(int idDocumento);
        Task GuardarCambiosAsync();
        Task ActualizarDescargarAsync(int idDocumento, bool permitirDescarga);
       
        Task<bool> DeshabilitarAsync(int id);

        Task RegistrarBitacoraAsync(TBitacoraEventos evento);

        Task ActualizarBatchAsync(List<TDocumentosCita> documentos);

        int InsertarDocumentoCifrado(TDocumentosCita doc);
        void GuardarEncryptedKey(TDocumentoKey key);
        TDocumentoKey ObtenerEncryptedKey(int documentoId, string usuarioId);
        TDocumentoKey ObtenerEncryptedKeyPorDocumento(int documentoId, string usuarioId);
        Task<TClavePublica> ObtenerClavePublicaUsuario(string usuarioId);
        Task<TClavePublica> GenerarClavePublicaUsuario(string usuarioId);
        Task GuardarClavePublica(TClavePublica entidad);
        Task InsertarDocumentoCifradoAsync(TDocumentosCita doc);
        Task GuardarEncryptedKeyAsync(TDocumentoKey key);
        Task<bool> ExisteCitaAsync(int idCita);
        Task<List<string>> ObtenerUsuariosDeCitaAsync(int idCita, UserManager<IdentityUser> userManager);
        bool ExisteClavePublica(string usuarioId);
    }
}
