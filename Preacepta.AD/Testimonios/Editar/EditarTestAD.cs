using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Testimonios.Editar
{
    public class EditarTestAD : IEditarTestAD
    {
        private readonly Contexto _contexto;

        public EditarTestAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> editar(TTestimonio test)
        {
            if (test == null) return 0;

            try
            {
                // Opción 1: Actualizar solo campos específicos (recomendado)
                var existingTest = await _contexto.TTestimonios.FindAsync(test.IdTestimonio);
                if (existingTest == null) return 0;

                _contexto.Entry(existingTest).CurrentValues.SetValues(test);
                // O actualizar campo por campo:
                // existingTest.Activo = test.Activo;
                // existingTest.Comentario = test.Comentario;
                // etc...

                // Opción 2: Adjuntar y marcar como modificado
                // _contexto.Attach(test);
                // _contexto.Entry(test).State = EntityState.Modified;

                return await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarTestimonioAD: {ex.Message}");
                return -1;
            }
        }
    }
}
