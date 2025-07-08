using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GePersona.Editar
{
    public class EditarGePersonaAD : IEditarGePersonaAD
    {
        private readonly Contexto _contexto;


        public EditarGePersonaAD(Contexto contexto)
        {
            _contexto = contexto;
        }



        /*public async Task<int> editar(TGePersona gePersona)
        {
            if (gePersona == null)
            {
                return 0;
            }

            try
            {
                _contexto.TGePersonas.Update(gePersona);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarGePersonaAD : {ex.Message}");
                return 0;
            }

        }*/


        public async Task<int> editar(TGePersona gePersona)
        {
            if (gePersona == null)
                return 0;

            try
            {
                // Verifica si ya hay una entidad con la misma clave rastreada
                var local = _contexto.TGePersonas.Local
                    .FirstOrDefault(p => p.Cedula == gePersona.Cedula);

                if (local != null)
                {
                    _contexto.Entry(local).State = EntityState.Detached;
                }

                // Recupera la entidad original desde la base
                var existente = await _contexto.TGePersonas
                    .FirstOrDefaultAsync(p => p.Cedula == gePersona.Cedula);

                if (existente == null)
                    return 0;

                // Actualiza las propiedades necesarias
                _contexto.Entry(existente).CurrentValues.SetValues(gePersona);

                return await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"Concurrencia detectada: {ex.Message}");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarGePersonaAD : {ex.Message}");
                return 0;
            }
        }

    }
}
