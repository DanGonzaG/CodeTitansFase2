using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.CasosEtapa.Editar
{
    public interface IEditarCasosEtapasAD
    {
        Task<int> Editar(TCasosEtapa editar);
    }
}
