using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.CasosTipo.Editar
{
    public interface IEditarCasosTiposAD
    {
        Task<int> Editar(TCasosTipo editar);
    }
}
