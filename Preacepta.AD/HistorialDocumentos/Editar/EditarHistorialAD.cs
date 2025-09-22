using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.HistorialDocumentos.Editar
{
    public class EditarHistorialAD : IEditarHistorialAD
    {
        private readonly Contexto _contexto;
        public EditarHistorialAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> Editar(THistorialDocumento editar)
        {
            if (editar == null) return 0;

            try
            {
                var local = _contexto.THistorialDocumentos.Local
                    .FirstOrDefault(x => x.Id == editar.Id);
                if (local != null)
                {
                    _contexto.Entry(local).State = EntityState.Detached;
                }

                var enBD = await _contexto.THistorialDocumentos
                    .FirstOrDefaultAsync(x => x.Id == editar.Id);

                if (enBD == null) return 0;

                editar.TipoDocumento = enBD.TipoDocumento;
                editar.IdDocumento = enBD.IdDocumento;

                if (editar.Fecha == default(DateTime))
                    editar.Fecha = enBD.Fecha;
                // -----------------------------------------------

                _contexto.Entry(enBD).CurrentValues.SetValues(editar);
                var bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"Concurrencia en EditarHistorialAD: {ex.Message}");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarHistorialAD: {ex.Message}");
                return 0;
            }
        }
    }
}
