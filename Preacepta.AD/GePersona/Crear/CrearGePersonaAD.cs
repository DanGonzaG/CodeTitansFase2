using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GePersona.Crear
{
    public class CrearGePersonaAD : ICrearGePersonaAD
    {
        private readonly Contexto _contexto;

        public CrearGePersonaAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TGePersona gePersona)
        {
            await _contexto.TGePersonas.AddAsync(gePersona);
            int guardado = await _contexto.SaveChangesAsync();
            return guardado;
        }
    }
}
