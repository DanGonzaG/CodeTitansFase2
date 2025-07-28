using Preacepta.AD.Citas.BuscarXid;
using Preacepta.AD.Citas.Eliminar;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.Citas.Eliminar
{
    public class EliminarCitasAD : IEliminarCitasAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarCitasAD _buscarXid;

        public EliminarCitasAD(Contexto contexto, IBuscarCitasAD buscarXid)
        {
            _contexto = contexto;
            _buscarXid = buscarXid;
        }

        public async Task<int> Eliminar(int id)
        {

            try
            {
                TCita? encontrado = await _buscarXid.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                var documentos = _contexto.TDocumentosCita.Where(d => d.IdCita == id);
                _contexto.TDocumentosCita.RemoveRange(documentos);

                var relaciones = _contexto.TCitasClientes.Where(cc => cc.IdCita == id);
                _contexto.TCitasClientes.RemoveRange(relaciones);

                _contexto.TCitas.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarCitasAD: {ex.Message}");
                return -1;
            }

        }
    }
}