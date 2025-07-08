using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return await _contexto.TDocumentosCita.FindAsync(idDocumento);
        }

        public async Task GuardarCambiosAsync()
        {
            await _contexto.SaveChangesAsync();
        }
    }

}
