using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.CrDireccion1.Editar
{
    public class EditarCrDireccion1AD : IEditarCrDireccion1AD
    {
        private readonly Contexto _contexto;
        public EditarCrDireccion1AD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> EditarProvincia(TCrProvincia editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                _contexto.TCrProvincias.Update(editar);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarCrDireccion1AD-Editar-TCrProvincia : {ex.Message}");
                return -1;
            }
        }

        public async Task<int> EditarCanton(TCrCantone editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                _contexto.TCrCantones.Update(editar);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarCrDireccion1AD-Editar-TCrCantone : {ex.Message}");
                return -1;
            }
        }

        public async Task<int> EditarDistrito(TCrDistrito editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                _contexto.TCrDistritos.Update(editar);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarCrDireccion1AD-Editar-TCrDistrito : {ex.Message}");
                return -1;
            }
        }
    }
}
