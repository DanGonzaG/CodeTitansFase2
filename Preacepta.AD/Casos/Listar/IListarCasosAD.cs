using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD.Casos.Listar
{
    public interface IListarCasosAD
    {
        Task<List<CasoDTO>> listar();

        Task<List<CasoDTO>> listarXabogado(int cedula);
    }
}
