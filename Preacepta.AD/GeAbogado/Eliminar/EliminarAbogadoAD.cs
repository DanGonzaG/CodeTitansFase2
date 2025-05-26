using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.GeAbogado.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeAbogado.Eliminar
{
    public class EliminarAbogadoAD : IEliminarAbogadoAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarAbogadoAD _buscarXid;

        public EliminarAbogadoAD(Contexto contexto, IBuscarAbogadoAD buscarXid)
        {
            _contexto = contexto;
            _buscarXid = buscarXid;
        }

        public async Task<int> Eliminar(int id)
        {

            try
            {
                TGeAbogado? encontrado = await _buscarXid.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TGeAbogados.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarAbogadoAD: {ex.Message}");
                return -1;
            }

        }
    }
}
