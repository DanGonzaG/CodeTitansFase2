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

    }
}
