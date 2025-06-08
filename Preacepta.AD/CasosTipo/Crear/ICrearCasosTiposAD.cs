using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.CasosTipo.Crear
{
    public interface ICrearCasosTiposAD
    {
        Task<int> crear(TCasosTipo crear);
    }
}
