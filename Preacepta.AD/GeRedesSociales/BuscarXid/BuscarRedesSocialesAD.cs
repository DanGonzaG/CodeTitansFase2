using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.AD.GeRedesSociales.BuscarXid
{
    public class BuscarRedesSocialesAD : IBuscarRedesSocialesAD
    {
        private readonly Contexto _contexto;

        public BuscarRedesSocialesAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TGeRedesSociale?> buscar(int id)
        {
            try
            {
                var lista = await _contexto.TGeRedesSociales
                .Include(t => t.CedulaNavigation)
                .FirstOrDefaultAsync(m => m.IdRs == id); ;
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarRedesSocialesAD, no se encontro id: {ex.Message}");
                return null;
            }

        }
    }
}
