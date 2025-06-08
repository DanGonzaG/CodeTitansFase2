using Preacepta.AD.CitasTipo.BuscarXid;
using Preacepta.AD.CitasTipo.Eliminar;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CitasTipo.Eliminar
{
    public class EliminarCitasTipoAD : IEliminarCitasTipoAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarCitasTipoAD _buscarXid;

        public EliminarCitasTipoAD(Contexto contexto, IBuscarCitasTipoAD buscarXid)
        {
            _contexto = contexto;
            _buscarXid = buscarXid;
        }

        public async Task<int> eliminar(int id)
        {

            try
            {
                TCitasTipo? encontrado = await _buscarXid.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TCitasTipos.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarCitasTipoAD: {ex.Message}");
                return -1;
            }

        }
    }
}