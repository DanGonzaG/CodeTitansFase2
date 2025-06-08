using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeRedesSociales.Editar
{
    public interface IEditarRedesSocialesAD
    {
        Task<int> Editar(TGeRedesSociale editar);
    }
}
