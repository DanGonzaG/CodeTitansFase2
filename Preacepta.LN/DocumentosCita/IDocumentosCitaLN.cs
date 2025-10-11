using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Preacepta.LN.DocumentosCita
{
    public interface IDocumentosCitaLN
    {
        List<DocumentosCitaDTO> ObtenerPorCita(int idCita);

        Task<TDocumentosCita> SubirDocumentoAsync(int idCita, string nombreArchivo, string rutaArchivo);

        Task ActualizarPermisoDescargaAsync(int idDocumento, bool permitirDescarga);

        Task<DocumentosCitaDTO> ObtenerPorIdAsync(int idDocumento);

        Task<bool> DeshabilitarAsync(int id);

        Task RegistrarBitacoraAsync(string usuario, string tabla, string accion, int idRegistro, string? stackError = null);

        Task ActualizarBatchAsync(List<DocumentosCitaDTO> documentos);
    }
}
