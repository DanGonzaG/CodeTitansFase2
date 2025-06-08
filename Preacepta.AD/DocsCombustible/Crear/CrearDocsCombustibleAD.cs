using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsCombustible.Crear
{
    public class CrearDocsCombustibleAD : ICrearDocsCombustibleAD
    {
        private readonly Contexto _contexto;

        public CrearDocsCombustibleAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TDocsCombustible crear)
        {
            if (crear == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TDocsCombustibles.AddAsync(crear);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearDocsCombustibleAD {ex.Message}");
                return 0;
            }


        }
    }
}
