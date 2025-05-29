using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CasosEtapa.Crear
{
    public class CrearCasosEtapasAD : ICrearCasosEtapasAD
    {
        private readonly Contexto _contexto;

        public CrearCasosEtapasAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TCasosEtapa crear)
        {
            if (crear == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TCasosEtapas.AddAsync(crear);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearCasosEtapasAD {ex.Message}");
                return 0;
            }
        }
    }
}
