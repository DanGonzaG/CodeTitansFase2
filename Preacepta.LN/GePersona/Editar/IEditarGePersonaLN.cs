using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GePersona.Editar
{
    public interface IEditarGePersonaLN
    {
        Task<int> editar(GePersonaDTO gePersonaDTO);
        Task<int> ActualizarFechaPassword(GePersonaDTO gePersonaDTO);
    }
}
