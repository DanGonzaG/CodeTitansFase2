using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.Casos.Editar
{
    public interface IEditarCasosLN
    {
        Task<int> Editar(CasoDTO editar);
    }
}
