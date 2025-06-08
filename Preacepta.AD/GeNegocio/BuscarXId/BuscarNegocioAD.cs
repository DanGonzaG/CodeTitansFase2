using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeNegocio.BuscarXId
{
    public class BuscarNegocioAD : IBuscarNegocioAD
    {
        private readonly Contexto _contexto;

        public BuscarNegocioAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TGeNegocio?> buscar(int id)
        {
            try
            {
                var lista = await _contexto.TGeNegocios.FindAsync(id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarNegocioAD, no se encontro id: {ex.Message}");
                return null;
            }

        }
    }
}
