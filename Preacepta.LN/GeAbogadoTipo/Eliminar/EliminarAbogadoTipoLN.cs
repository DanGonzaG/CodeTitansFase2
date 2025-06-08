using Preacepta.AD.GeAbogadoTipo.Eliminar;

namespace Preacepta.LN.GeAbogadoTipo.Eliminar
{
    public class EliminarAbogadoTipoLN : IEliminarAbogadoTipoLN
    {
        private readonly IEliminarAbogadoTipoAD _eliminar;

        public EliminarAbogadoTipoLN(IEliminarAbogadoTipoAD eliminar)
        {
            _eliminar = eliminar;
        }

        public async Task<int> eliminar(int id)
        {
            if (id < 0)
            {
                Console.WriteLine("el valor de id en menor a 1");
                return 0;
            }
            try
            {
                int bandera = await _eliminar.eliminar(id);
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en: EliminarPersonaLN {ex.Message}");
                return -1;
            }


        }
    }
}

