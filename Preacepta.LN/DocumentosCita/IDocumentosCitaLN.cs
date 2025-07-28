using Microsoft.AspNetCore.Http;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System.Collections.Generic;

namespace Preacepta.LN.DocumentosCita
{
    public interface IDocumentosCitaLN
    {
        List<DocumentosCitaDTO> ObtenerPorCita(int idCita);
        void SubirArchivo(int idCita, IFormFile archivo);
        Task ActualizarPermisoDescargaAsync(int idDocumento, bool permitirDescarga);
        Task<DocumentosCitaDTO> ObtenerPorIdAsync(int idDocumento);
    }
}
