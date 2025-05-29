using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeRedesSociales.Crear
{
    public class CrearRedesSocialesAD : ICrearRedesSocialesAD
    {
        private readonly Contexto _contexto;

        public CrearRedesSocialesAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TGeRedesSociale crear)
        {
            if (crear == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TGeRedesSociales.AddAsync(crear);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearRedesSocialesAD {ex.Message}");
                return 0;
            }
        }
    }
}
