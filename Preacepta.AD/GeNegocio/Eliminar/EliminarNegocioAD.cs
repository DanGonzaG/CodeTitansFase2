using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.GeNegocio.BuscarXId;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.GeNegocio.Eliminar
{
    public class EliminarNegocioAD : IEliminarNegocioAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarNegocioAD _buscarXid;

        public EliminarNegocioAD(Contexto contexto, IBuscarNegocioAD buscarXid)
        {
            _contexto = contexto;
            _buscarXid = buscarXid;
        }

        public async Task<int> Eliminar(int id)
        {

            try
            {
                TGeNegocio? encontrado = await _buscarXid.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TGeNegocios.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarNegocioAD: {ex.Message}");
                return -1;
            }

        }
    }
}
