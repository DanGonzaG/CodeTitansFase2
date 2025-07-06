using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.Casos.Listar
{
    public interface IListarCasosLN
    {
        Task<List<CasoDTO>> listar();
        Task<List<CasoDTO>> listarXabogado(int cedula);
        Task<List<CasoDTO>> listarXcliente(int cedula);

        Task<CasoDTO> listarXultimaFecha(int cedula);
    }
}
