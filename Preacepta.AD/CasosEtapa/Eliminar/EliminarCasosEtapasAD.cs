using Preacepta.AD.CasosEtapa.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.CasosEtapa.Eliminar
{
    public class EliminarCasosEtapasAD : IEliminarCasosEtapasAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarCasosEtapasAD _buscarXid;

        public EliminarCasosEtapasAD(Contexto contexto, IBuscarCasosEtapasAD buscarXid)
        {
            _contexto = contexto;
            _buscarXid = buscarXid;
        }

        public async Task<int> Eliminar(int id)
        {

            try
            {
                TCasosEtapa? encontrado = await _buscarXid.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TCasosEtapas.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarCasosEtapasAD: {ex.Message}");
                return -1;
            }

        }
    }
}
