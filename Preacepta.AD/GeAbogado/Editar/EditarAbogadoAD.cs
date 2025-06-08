using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeAbogado.Editar
{
    public class EditarAbogadoAD : IEditarAbogadoAD
    {
        private readonly Contexto _contexto;
        public EditarAbogadoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> Editar(TGeAbogado editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                _contexto.TGeAbogados.Update(editar);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarAbogadoAD : {ex.Message}");
                return -1;
            }

        }
    }
}
