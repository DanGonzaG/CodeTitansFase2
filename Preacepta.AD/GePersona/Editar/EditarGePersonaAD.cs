using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GePersona.Editar
{
    public class EditarGePersonaAD : IEditarGePersonaAD
    {
        private readonly Contexto _contexto;
        

        public EditarGePersonaAD (Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> editar(TGePersona gePersona) 
        {
            if (gePersona == null) 
            {
                return 0;
            }

            try
            {
                _contexto.TGePersonas.Update(gePersona);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            } catch (Exception ex) 
            {
                Console.WriteLine($"Error en EditarGePersonaAD : {ex.Message}");
                return 0;
            }
            
        }
         
    }
}
