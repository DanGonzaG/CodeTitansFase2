using Preacepta.AD.GeAbogadoTipo.BuscarXid;
using Preacepta.AD.GePersona.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeAbogadoTipo.Eliminar
{
    public class EliminarAbogadoTipoAD : IEliminarAbogadoTipoAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarAbogadoTipoAD _buscarXid;

        public EliminarAbogadoTipoAD(Contexto contexto, IBuscarAbogadoTipoAD buscarXidGePersonaAD)
        {
            _contexto = contexto;
            _buscarXid = buscarXidGePersonaAD;
        }

        public async Task<int> eliminar(int id)
        {

            try
            {
                TGeAbogadoTipo? tipoAbogado = await _buscarXid.buscar(id);
                if (tipoAbogado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TGeAbogadoTipos.Remove(tipoAbogado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarAbogadoTipoAD: {ex.Message}");
                return -1;
            }

        }
    }
}
