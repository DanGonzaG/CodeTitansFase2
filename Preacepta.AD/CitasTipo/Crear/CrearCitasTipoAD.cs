using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CitasTipo.Crear
{
    
    public class CrearCitasTipoAD : ICrearCitasTipoAD
    {
        private readonly Contexto _contexto;

        public CrearCitasTipoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TCitasTipo citatipo)
        {
            if (crear == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TCitasTipos.AddAsync(citatipo);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearCitasTiposAD {ex.Message}");
                return 0;
            }
        }

     
    }
}
