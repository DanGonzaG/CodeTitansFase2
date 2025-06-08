namespace Preacepta.LN.CrDireccion1.Eliminar
{
    public interface IEliminarCrDireccion1LN
    {
        Task<int> EliminarProvincia(int id);
        Task<int> EliminarCanton(int id);
        Task<int> EliminarDistrito(int id);
    }
}
