using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GePersona.Crear
{
    public interface ICrearGePersonaLN
    {
        Task<int> crear(GePersonaDTO gePersonaDTO);
    }
}
