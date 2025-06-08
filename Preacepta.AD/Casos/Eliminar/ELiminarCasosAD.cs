using Preacepta.AD.Casos.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.Casos.Eliminar
{
    public class ELiminarCasosAD : IELiminarCasosAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarCasosAD _buscarXid;

        public ELiminarCasosAD(Contexto contexto, IBuscarCasosAD buscarXid)
        {
            _contexto = contexto;
            _buscarXid = buscarXid;
        }

        public async Task<int> Eliminar(int id)
        {

            try
            {
                TCaso? encontrado = await _buscarXid.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TCasos.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ELiminarCasosAD: {ex.Message}");
                return -1;
            }

        }
    }
}
