using Preacepta.AD.GeRedesSociales.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeRedesSociales.Eliminar
{
    public class EliminarRedesSocialesAD : IEliminarRedesSocialesAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarRedesSocialesAD _buscarXid;

        public EliminarRedesSocialesAD(Contexto contexto, IBuscarRedesSocialesAD buscarXid)
        {
            _contexto = contexto;
            _buscarXid = buscarXid;
        }

        public async Task<int> Eliminar(int id)
        {

            try
            {
                TGeRedesSociale? encontrado = await _buscarXid.buscar(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TGeRedesSociales.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarRedesSocialesAD: {ex.Message}");
                return -1;
            }

        }
    }
}
