using Preacepta.AD.CasosEtapa.Eliminar;

namespace Preacepta.LN.CasosEtapa.Eliminar
{
    public class EliminarCasosEtapasLN : IEliminarCasosEtapasLN
    {
        private readonly IEliminarCasosEtapasAD _eliminar;

        public EliminarCasosEtapasLN(IEliminarCasosEtapasAD eliminar)
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
                Console.WriteLine($"Error en: EliminarCasosEtapasLN {ex.Message}");
                return -1;
            }
        }
    }
}
