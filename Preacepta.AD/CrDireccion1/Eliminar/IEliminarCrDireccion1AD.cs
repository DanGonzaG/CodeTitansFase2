namespace Preacepta.AD.CrDireccion1.Eliminar
{
    public interface IEliminarCrDireccion1AD
    {
        Task<int> EliminarProvincia(int id);
        Task<int> EliminarCanton(int id);
        Task<int> EliminarDistrito(int id);
    }
}
