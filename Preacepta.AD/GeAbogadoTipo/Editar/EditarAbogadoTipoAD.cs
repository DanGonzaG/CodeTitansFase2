using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeAbogadoTipo.Editar
{
    public class EditarAbogadoTipoAD : IEditarAbogadoTipoAD
    {
        private readonly Contexto _contexto;
        public EditarAbogadoTipoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> editar(TGeAbogadoTipo geAbogadoTipo)
        {
            if (geAbogadoTipo == null)
            {
                return 0;
            }

            try
            {
                _contexto.TGeAbogadoTipos.Update(geAbogadoTipo);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarAbogadoTipoAD : {ex.Message}");
                return -1;
            }

        }
    }
}
