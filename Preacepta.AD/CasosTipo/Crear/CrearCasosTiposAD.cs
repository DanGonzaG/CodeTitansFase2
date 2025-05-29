using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CasosTipo.Crear
{
    public class CrearCasosTiposAD : ICrearCasosTiposAD
    {
        private readonly Contexto _contexto;

        public CrearCasosTiposAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TCasosTipo crear)
        {
            if (crear == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TCasosTipos.AddAsync(crear);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearCasosTiposAD {ex.Message}");
                return 0;
            }
        }
    }
}
