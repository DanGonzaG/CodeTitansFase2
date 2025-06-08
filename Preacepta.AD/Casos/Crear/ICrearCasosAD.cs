using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.Casos.Crear
{
    public interface ICrearCasosAD
    {
        Task<int> crear(TCaso crear);
    }
}
