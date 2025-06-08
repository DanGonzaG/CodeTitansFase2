using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeAbogado.Crear
{
    public interface ICrearAbogadoLN
    {
        Task<int> Crear(PersonaUnionAbogado crear);
    }
}
