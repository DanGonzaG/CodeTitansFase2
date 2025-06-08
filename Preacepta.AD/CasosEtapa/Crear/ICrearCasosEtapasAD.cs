using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.CasosEtapa.Crear
{
    public interface ICrearCasosEtapasAD
    {
        Task<int> crear(TCasosEtapa crear);
    }
}
