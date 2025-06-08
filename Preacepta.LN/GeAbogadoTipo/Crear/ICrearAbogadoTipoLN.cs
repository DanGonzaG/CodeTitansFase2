using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeAbogadoTipo.Crear
{
    public interface ICrearAbogadoTipoLN
    {
        Task<int> crear(GeAbogadoTipoDTO geAbogadoTipoDTO);
    }
}
