using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.CasosEvidencia.Editar
{
    public class EditarCasosEvidenciaAD : IEditarCasosEvidenciaAD
    {
        private readonly Contexto _contexto;
        public EditarCasosEvidenciaAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> Editar(TCasosEvidencia editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                _contexto.TCasosEvidencias.Update(editar);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarCasosEvidenciaAD : {ex.Message}");
                return -1;
            }
        }
    }
}
