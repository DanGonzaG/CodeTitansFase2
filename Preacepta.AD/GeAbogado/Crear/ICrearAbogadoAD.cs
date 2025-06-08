using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeAbogado.Crear
{
    public interface ICrearAbogadoAD
    {
        Task<int> crear(TGeAbogado crear);
    }
}
