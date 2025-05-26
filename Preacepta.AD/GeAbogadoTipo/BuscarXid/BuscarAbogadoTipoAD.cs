using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeAbogadoTipo.BuscarXid
{
    public class BuscarAbogadoTipoAD : IBuscarAbogadoTipoAD
    {
        private readonly Contexto _contexto;

        public BuscarAbogadoTipoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TGeAbogadoTipo?> buscar(int id)
        {
            try
            {
                var tGetipoAbogado = await _contexto.TGeAbogadoTipos.FindAsync(id);
                return tGetipoAbogado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarAbogadoTipoAD, no se encontro id: {ex.Message}");
                return null;
            }

        }

    }
}
