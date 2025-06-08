using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.Casos.Editar
{
    public class EditarCasosAD : IEditarCasosAD
    {
        private readonly Contexto _contexto;
        public EditarCasosAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> Editar(TCaso editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                _contexto.TCasos.Update(editar);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarCasosAD : {ex.Message}");
                return -1;
            }

        }
    }
}
