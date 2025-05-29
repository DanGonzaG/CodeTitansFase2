using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Casos.Crear
{
    public class CrearCasosAD : ICrearCasosAD
    {
        private readonly Contexto _contexto;

        public CrearCasosAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TCaso crear)
        {
            if (crear == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TCasos.AddAsync(crear);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearCasosAD {ex.Message}");
                return 0;
            }
        }
    }
}
