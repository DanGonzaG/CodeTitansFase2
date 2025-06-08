using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GePersona.Editar
{
    public interface IEditarGePersonaAD
    {
        Task<int> editar(TGePersona gePersona);
    }
}
