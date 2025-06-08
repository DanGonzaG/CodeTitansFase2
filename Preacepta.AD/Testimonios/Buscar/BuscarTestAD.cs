using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Testimonios.Buscar
{
    public class BuscarTestAD : IBuscarTestAD
    {
        private readonly Contexto _contexto;

        public BuscarTestAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TTestimonio?> buscar(int id)
        {
            try
            {
                var lista = await _contexto.TTestimonios.FindAsync(id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarTestimoniosAD, no se encontro id: {ex.Message}");
                return null;
            }
        }

        public async Task<TGePersona?> BuscarPorId(int idCliente)
        {
            var nombre = await _contexto.TGePersonas.FindAsync(idCliente);
            return nombre;
        }

    }
}
