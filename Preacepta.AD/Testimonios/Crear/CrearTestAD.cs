using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Testimonios.Crear
{
    public class CrearTestAD : ICrearTestAD
    {
        private readonly Contexto _contexto;

        public CrearTestAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TTestimonio test)
        {
            if (test == null)
            {
                Console.WriteLine("El objeto recibido fue nulo");
                return -1;
            }

            try
            {
                // DEBUG: Verifica la entidad antes de guardar
                Console.WriteLine($"Entidad a guardar - Activo: {test.Activo}");

                // Configurar el logging de consultas SQL (para EF Core)
                _contexto.Database.SetCommandTimeout(120); // Opcional: aumentar timeout si es necesario
                var debugView = _contexto.ChangeTracker.DebugView; // Para ver el estado del contexto

                await _contexto.TTestimonios.AddAsync(test);
                int guardado = await _contexto.SaveChangesAsync();

                // DEBUG: Verifica la entidad después de guardar
                Console.WriteLine($"Entidad guardada - Activo: {test.Activo}, ID: {test.IdTestimonio}");

                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearTestAD: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return 0;
            }
        }
    }
}
