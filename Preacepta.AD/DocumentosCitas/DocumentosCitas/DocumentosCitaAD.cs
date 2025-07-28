using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // Método para actualizar la propiedad Descargar
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

        // Esta acción también puede estar en la capa de lógica de negocio (LN) en lugar de aquí.
        // Si es necesaria, está bien, pero es redundante con el anterior.
        public async Task ActualizarPermisoDescargaAsync(int idDocumento, bool permitirDescarga)
        {
            var documento = await _contexto.TDocumentosCita.FindAsync(idDocumento);
            if (documento == null)
                throw new Exception("Documento no encontrado");

            documento.Descargar = permitirDescarga;
            await _contexto.SaveChangesAsync();  // Guarda los cambios en la base de datos
        }
    }
}
