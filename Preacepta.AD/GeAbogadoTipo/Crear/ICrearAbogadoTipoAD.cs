using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeAbogadoTipo.Crear
{
    public interface ICrearAbogadoTipoAD
    {
        Task<int> crear(TGeAbogadoTipo geAbogadoTipo);
    }
}
