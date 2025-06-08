using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.GeRedesSociales.Crear
{
    public interface ICrearRedesSocialesLN
    {
        Task<int> Crear(GeRedesSocialeDTO crear);
    }
}
