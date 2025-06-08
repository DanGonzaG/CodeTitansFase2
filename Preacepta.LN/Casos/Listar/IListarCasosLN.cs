using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.Casos.Listar
{
    public interface IListarCasosLN
    {
        Task<List<CasoDTO>> listar();
    }
}
