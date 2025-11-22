using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GePersona.BuscarXid
{
    public interface IBuscarXidGePersonaAD
    {
        //Metodos para retornar objetos completos
        Task<TGePersona?> buscar(int id);

        Task<TGePersona?> buscarXnumCedula(string id);

        Task<TGePersona?> buscarXcorreo(string correo);

        Task<TGePersona?> buscarXtelefono1(string telefono);

        Task<TGePersona?> buscarXtelefono2(string telefono);


        //Metodo Booleanos para corroborar existencia del objeto
        Task<bool?> buscarXnumCedulaBOOLEAN(string id);

        Task<bool?> buscarXcorreoBOOLEAN(string correo);

        Task<bool?> buscarXtelefono1BOOLEAN(string telefono);

        Task<bool?> buscarXtelefono2BOOLEAN(string telefono);
    }
}
