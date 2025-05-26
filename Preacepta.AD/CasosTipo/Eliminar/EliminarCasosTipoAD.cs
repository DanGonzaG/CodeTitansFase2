using Preacepta.AD.CasosTipo.BuscarXid;
using Preacepta.AD.GeAbogadoTipo.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CasosTipo.Eliminar
{
    public class EliminarCasosTipoAD : IEliminarCasosTipoAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarCasosTiposAD _buscarXid;

        public EliminarCasosTipoAD(Contexto contexto, IBuscarCasosTiposAD buscarXid)
        {
            _contexto = contexto;
            _buscarXid = buscarXid;
        }

        public async Task<int> Eliminar(int id)
        {

            try
            {
                TCasosTipo? encontrado = await _buscarXid.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TCasosTipos.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarCasosTipoAD: {ex.Message}");
                return -1;
            }

        }
    }
}
