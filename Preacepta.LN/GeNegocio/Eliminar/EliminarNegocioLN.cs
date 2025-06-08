using Preacepta.AD.GeNegocio.Eliminar;

namespace Preacepta.LN.GeNegocio.Eliminar
{
    public class EliminarNegocioLN : IEliminarNegocioLN
    {
        private readonly IEliminarNegocioAD _eliminar;

        public EliminarNegocioLN(IEliminarNegocioAD eliminar)
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
                Console.WriteLine($"Error en: EliminarNegocioLN {ex.Message}");
                return -1;
            }


        }
    }
}
