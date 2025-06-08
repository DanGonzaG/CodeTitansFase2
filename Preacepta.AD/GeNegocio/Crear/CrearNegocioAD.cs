using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeNegocio.Crear
{
    public class CrearNegocioAD : ICrearNegocioAD
    {
        private readonly Contexto _contexto;

        public CrearNegocioAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TGeNegocio crear)
        {
            if (crear == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TGeNegocios.AddAsync(crear);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearNegocioAD {ex.Message}");
                return 0;
            }
        }
    }
}
