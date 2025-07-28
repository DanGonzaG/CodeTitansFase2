using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD.Casos.Listar
{
    public interface IListarCasosAD
    {
        Task<List<CasoDTO>> listar();

        Task<List<CasoDTO>> listarXabogado(int cedula);

        Task<List<CasoDTO>> listarXcliente(int cedula);

        Task<CasoDTO> listarXultimaFecha(int cedula);

        Task<List<CasoDTO>> listarXclienteLos3Casos(int cedula);

        Task<List<CasoDTO>> listarXabogadoLos3Casos(int cedula);
    }
}
