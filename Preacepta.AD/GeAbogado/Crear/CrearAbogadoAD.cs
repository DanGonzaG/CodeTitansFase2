using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeAbogado.Crear
{
    public class CrearAbogadoAD : ICrearAbogadoAD
    {
        private readonly Contexto _contexto;

        public CrearAbogadoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TGeAbogado crear)
        {
            if (crear == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TGeAbogados.AddAsync(crear);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearAbogadoAD {ex.Message}");
                return 0;
            }


        }
    }
}
