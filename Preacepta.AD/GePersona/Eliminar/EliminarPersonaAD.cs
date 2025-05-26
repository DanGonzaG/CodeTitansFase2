using Preacepta.AD.GePersona.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GePersona.Eliminar
{
    public class EliminarPersonaAD : IEliminarPersonaAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarXidGePersonaAD _buscarXid;

        public EliminarPersonaAD (Contexto contexto, IBuscarXidGePersonaAD buscarXidGePersonaAD) 
        {
            _contexto = contexto;
            _buscarXid = buscarXidGePersonaAD;
        }

        public async Task<int> eliminar (int id) 
        {
           
            try
            {
                TGePersona? persona = await _buscarXid.buscar(id);
                if (persona == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TGePersonas.Remove(persona);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error en EliminarPersonaAD: {ex.Message}");
                return -1;
            }
           
        }
    }
}
