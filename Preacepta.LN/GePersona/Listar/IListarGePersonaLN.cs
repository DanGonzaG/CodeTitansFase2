using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GePersona.Listar
{
    public interface IListarGePersonaLN
    {
        Task<List<GePersonaDTO>> listar();
    }
}
