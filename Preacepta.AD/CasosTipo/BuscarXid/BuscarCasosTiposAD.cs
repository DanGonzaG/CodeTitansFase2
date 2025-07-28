using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.CasosTipo.BuscarXid
{
    public class BuscarCasosTiposAD : IBuscarCasosTiposAD
    {
        private readonly Contexto _contexto;

        public BuscarCasosTiposAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TCasosTipo?> buscar(int id)
        {
            try
            {
                var lista = await _contexto.TCasosTipos.FindAsync(id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCasosTiposAD, no se encontro id: {ex.Message}");
                return null;
            }

        }
    }
}
