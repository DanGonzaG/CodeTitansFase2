using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD.GePersona.Listar
{
    public interface IListarGePersonaAD
    {
        Task<List<GePersonaDTO>> listar();
    }
}
