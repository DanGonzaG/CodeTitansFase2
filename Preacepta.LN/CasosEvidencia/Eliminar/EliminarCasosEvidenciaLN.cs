using Preacepta.AD.CasosEvidencia.Eliminar;

namespace Preacepta.LN.CasosEvidencia.Eliminar
{
    public class EliminarCasosEvidenciaLN : IEliminarCasosEvidenciaLN
    {
        private readonly IEliminarCasosEvidenciaAD _eliminar;

        public EliminarCasosEvidenciaLN(IEliminarCasosEvidenciaAD eliminar)
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
                Console.WriteLine($"Error en: EliminarCasosEvidenciaLN {ex.Message}");
                return -1;
            }
        }
    }
}
