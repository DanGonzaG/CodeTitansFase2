using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;

public interface IDocumentosCitaLN
{
    List<DocumentosCitaDTO> ObtenerPorCita(int idCita);
    Task<TDocumentosCita> SubirDocumentoAsync(int idCita, string nombreArchivo, string rutaArchivo);
    Task ActualizarPermisoDescargaAsync(int idDocumento, bool permitirDescarga);
    Task<DocumentosCitaDTO> ObtenerPorIdAsync(int idDocumento);
    Task<bool> DeshabilitarAsync(int id);
    Task ActualizarBatchAsync(List<DocumentosCitaDTO> documentos);

    // Seguridad
    Task<TClavePublica> ObtenerClavePublicaUsuario(string usuarioId);
    Task<TClavePublica> GenerarClavePublicaUsuario(string userId);
    Task GuardarClavePublicaUsuario(string usuarioId, string publicKeyBase64);
    bool ExisteClavePublica(string usuarioId);
    Task<TDocumentoKey> ObtenerKeyDocumentoUsuario(int documentoId, string usuarioId);
}
