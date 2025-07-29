using Preacepta.AD.GeAbogado.Eliminar;
using Preacepta.LN.GePersona.Eliminar;

namespace Preacepta.LN.GeAbogado.Eliminar
{
    public class EliminarAbogadoLN : IEliminarAbogadoLN
    {
        private readonly IEliminarAbogadoAD _eliminar;
        private readonly IEliminarPersonaLN _eliminarPersona;

        public EliminarAbogadoLN(IEliminarAbogadoAD eliminar, IEliminarPersonaLN eliminarPersona)
        {
            _eliminar = eliminar;
            _eliminarPersona = eliminarPersona;
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
                if(bandera == 0 || bandera == null) 
                {
                    return 0;
                }
                int bandera2 = await _eliminarPersona.eliminar(id);
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en: EliminarAbogadoLN {ex.Message}");
                return -1;
            }


        }
    }
}
