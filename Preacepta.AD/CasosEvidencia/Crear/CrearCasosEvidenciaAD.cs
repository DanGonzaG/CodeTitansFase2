using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CasosEvidencia.Crear
{
    public class CrearCasosEvidenciaAD : ICrearCasosEvidenciaAD
    {
        private readonly Contexto _contexto;

        public CrearCasosEvidenciaAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TCasosEvidencia crear)
        {
            if (crear == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TCasosEvidencias.AddAsync(crear);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearCasosEvidenciaAD {ex.Message}");
                return 0;
            }
        }
    }
}
