using Preacepta.AD.Casos.Eliminar;

namespace Preacepta.LN.Casos.Eliminar
{
    public class ELiminarCasosLN : IELiminarCasosLN
    {
        private readonly IELiminarCasosAD _eliminar;

        public ELiminarCasosLN(IELiminarCasosAD eliminar)
        {
            _eliminar = eliminar;
        }

        public async Task<int> Eliminar(int id)
        {
            if (id < 0)
            {
                Console.WriteLine("el valor de id en menor a 1");
                return 0;
            }
            try
            {
                int bandera = await _eliminar.Eliminar(id);
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en: ELiminarCasosLN {ex.Message}");
                return -1;
            }
        }
    }
}
