using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GePersona.Crear
{
    public interface ICrearGePersonaAD
    {
        Task<int> crear(TGePersona gePersona);
    }
}
