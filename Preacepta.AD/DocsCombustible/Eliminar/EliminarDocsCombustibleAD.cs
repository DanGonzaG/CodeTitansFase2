using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.DocsCombustible.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsCombustible.Eliminar
{
    public class EliminarDocsCombustibleAD : IEliminarDocsCombustibleAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarDocsCombustibleAD _buscarXid;

        public EliminarDocsCombustibleAD(Contexto contexto, IBuscarDocsCombustibleAD buscarXid)
        {
            _contexto = contexto;
            _buscarXid = buscarXid;
        }

        public async Task<int> Eliminar(int id)
        {

            try
            {
                TDocsCombustible? encontrado = await _buscarXid.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TDocsCombustibles.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarDocsCombustibleAD: {ex.Message}");
                return -1;
            }

        }
    }
}
