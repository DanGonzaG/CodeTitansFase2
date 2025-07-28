using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.Casos.Editar
{
    public class EditarCasosAD : IEditarCasosAD
    {
        private readonly Contexto _contexto;
        public EditarCasosAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> Editar(TCaso editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                var instaciaLocal = _contexto.TCasos.Local.FirstOrDefault(local => local.IdCaso == editar.IdCaso);
                if (instaciaLocal != null) 
                {
                    _contexto.Entry(instaciaLocal).State = EntityState.Detached;
                }

                var objetoDeBD = await _contexto.TCasos.FirstOrDefaultAsync(enBD => enBD.IdCaso == editar.IdCaso);
                if (objetoDeBD == null)                 
                {
                    return 0;
                }

                _contexto.Entry(objetoDeBD).CurrentValues.SetValues(editar);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;                
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"Concurrencia detectada, EditarGePersonaAD, Editar : {ex.Message}");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarGePersonaAD, Editar : {ex.Message}");
                return 0;
            }

        }
    }
}
