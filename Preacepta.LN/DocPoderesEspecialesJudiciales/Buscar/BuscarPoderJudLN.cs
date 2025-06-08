using Preacepta.AD.DocPoderesEspecialesJudiciales.Buscar;
using Preacepta.LN.DocPoderesEspecialesJudiciales.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocPoderesEspecialesJudiciales.Buscar
{
    public class BuscarPoderJudLN : IBuscarPoderJudLN
    {
        private readonly IBuscarPoderJudAD _buscar;
        private readonly IObtenerDatosPoderJudLN _obtenerDatos;

        public BuscarPoderJudLN(IBuscarPoderJudAD buscar, IObtenerDatosPoderJudLN obtenerDatos)
        {
            _buscar = buscar;
            _obtenerDatos = obtenerDatos;
        }

        public async Task<DocsPoderesEspecialesJudicialeDTO?> buscar(int id)
        {
            try
            {
                // Cambiar tipo aquí
                TDocsPoderesEspecialesJudiciale? resultadoBusqueda = await _buscar.buscar(id);

                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró el tipo de testimonio.");
                    return null;
                }
                DocsPoderesEspecialesJudicialeDTO obtenerDatos = _obtenerDatos.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarPoderJudLN: {ex.Message}");
                return null;
            }
        }

    }
}
