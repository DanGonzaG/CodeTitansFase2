using Preacepta.AD.DocPoderesEspecialesJudiciales.Buscar;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocPoderesEspecialesJudiciales.Eliminar
{
    public class EliminarPoderJudAD : IEliminarPoderJudAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarPoderJudAD _buscarPoderJudad;

        public EliminarPoderJudAD(Contexto contexto, IBuscarPoderJudAD buscarPoderJudad)
        {
            _contexto = contexto;
            _buscarPoderJudad = buscarPoderJudad;
        }

        public async Task<int> eliminar(int id)
        {
            try
            {
                TDocsPoderesEspecialesJudiciale? encontrado = await _buscarPoderJudad.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TDocsPoderesEspecialesJudiciales.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarPoderJudAD: {ex.Message}");
                return -1;
            }
        }

    }
}
