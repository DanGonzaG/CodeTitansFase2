using Preacepta.AD.CasosEtapa.BuscarXid;
using Preacepta.AD.CasosEvidencia.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CasosEvidencia.Eliminar
{
    public class EliminarCasosEvidenciaAD : IEliminarCasosEvidenciaAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarCasosEvidenciaAD _buscarXid;

        public EliminarCasosEvidenciaAD(Contexto contexto, IBuscarCasosEvidenciaAD buscarXid)
        {
            _contexto = contexto;
            _buscarXid = buscarXid;
        }

        public async Task<int> Eliminar(int id)
        {

            try
            {
                TCasosEvidencia? encontrado = await _buscarXid.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TCasosEvidencias.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarCasosEvidenciaAD: {ex.Message}");
                return -1;
            }

        }
    }
}
