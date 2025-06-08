using Preacepta.AD.CrDireccion1.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.CrDireccion1.Eliminar
{
    public class EliminarCrDireccion1AD : IEliminarCrDireccion1AD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarCrDireccion1AD _buscarXid;

        public EliminarCrDireccion1AD(Contexto contexto, IBuscarCrDireccion1AD buscarXid)
        {
            _contexto = contexto;
            _buscarXid = buscarXid;
        }

        public async Task<int> EliminarProvincia(int id)
        {
            try
            {
                TCrProvincia? encontrado = await _buscarXid.buscarProvincias(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TCrProvincias.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarCrDireccion1AD-Eliminar-EliminarProvincia: {ex.Message}");
                return -1;
            }
        }

        public async Task<int> EliminarCanton(int id)
        {
            try
            {
                TCrCantone? encontrado = await _buscarXid.buscarCantones(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TCrCantones.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarCrDireccion1AD-Eliminar-EliminarCanton: {ex.Message}");
                return -1;
            }
        }

        public async Task<int> EliminarDistrito(int id)
        {
            try
            {
                TCrDistrito? encontrado = await _buscarXid.buscarDistritos(id);
                if (encontrado == null)
                {
                    Console.WriteLine($"Buscar por id es nulo");
                    return 0;
                }
                _contexto.TCrDistritos.Remove(encontrado);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarCrDireccion1AD-Eliminar-EliminarDistrito: {ex.Message}");
                return -1;
            }
        }
    }
}
