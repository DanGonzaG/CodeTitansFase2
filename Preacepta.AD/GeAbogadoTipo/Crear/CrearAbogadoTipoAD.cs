using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeAbogadoTipo.Crear
{
    public class CrearAbogadoTipoAD : ICrearAbogadoTipoAD
    {
        private readonly Contexto _contexto;

        public CrearAbogadoTipoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TGeAbogadoTipo geAbogadoTipo)
        {
            if (geAbogadoTipo == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TGeAbogadoTipos.AddAsync(geAbogadoTipo);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error en CrearAbogadoTipoAD {ex.Message}");
                return 0;
            }

            
        }
    }
}
