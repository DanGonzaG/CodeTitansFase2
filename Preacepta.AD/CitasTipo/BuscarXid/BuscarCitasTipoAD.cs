using Microsoft.EntityFrameworkCore;
using Preacepta.AD.CitasTipo.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CitasTipo.BuscarXid

{
    public class BuscarCitasTipoAD : IBuscarCitasTipoAD
    {
        private readonly Contexto _contexto;

        public BuscarCitasTipoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TCitasTipo?> buscar(int id)
        {
            try
            {
                var citatipo = await _contexto.TCitasTipos.FindAsync(id);
                return citatipo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCitasAD, no se encontro id: {ex.Message}");
                return null;
            }

        }
    }
}
