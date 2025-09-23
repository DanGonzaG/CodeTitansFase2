using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.HistorialDocumentos.BuscarXid
{
    public class BuscarHistorialAD : IBuscarHistorialAD
    {
        private readonly Contexto _contexto;

        public BuscarHistorialAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        // Query base con Includes útiles para las vistas
        private IQueryable<THistorialDocumento> BaseQuery()
        {
            return _contexto.THistorialDocumentos
                .AsNoTracking()
                .Include(h => h.AbogadoNavigation)
                    .ThenInclude(a => a.CedulaNavigation)
                    .ThenInclude(p => p.Direccion1Navigation)
                .Include(h => h.ClienteNavigation)
                    .ThenInclude(p => p.Direccion1Navigation);
        }

        // Buscar por Id del historial
        public async Task<THistorialDocumento?> Buscar(int id)
        {
            try
            {
                return await BaseQuery().FirstOrDefaultAsync(h => h.Id == id);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"BuscarHistorialAD.Buscar: {ex.Message}");
                return null;
            }
        }

        // Buscar por (TipoDocumento, IdDocumento) SIN helper externo
        public async Task<THistorialDocumento?> BuscarPorDocumento(string tipoDocumento, int idDocumento)
        {
            try
            {
                string t = (tipoDocumento ?? string.Empty).Trim();

                // Validamos que sea uno de los tipos canónicos que guardas en la BD
                switch (t)
                {
                    case "T_DocsPagare":
                    case "T_DocsOpcionCompraventaVehiculo":
                    case "T_DocumentosCita":
                    case "T_DocsAutorizacionRevisionExpediente":
                    case "T_DocsCombustibles":
                    case "T_DocsCompraventaFinca":
                    case "T_DocsContratoPrestacionServicios":
                    case "T_DocsInscripcionVehiculo":
                    case "T_DocsMarcaVehiculo":
                    case "T_DocsPoderesEspecialesJudiciales":
                    case "T_DocsTipoVehiculos":
                        break; // válido
                    default:
                        // tipo desconocido → no se busca nada
                        return null;
                }

                return await BaseQuery()
                    .Where(h => h.TipoDocumento == t && h.IdDocumento == idDocumento)
                    .OrderByDescending(h => h.Fecha)
                    .FirstOrDefaultAsync();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"BuscarHistorialAD.BuscarPorDocumento: {ex.Message}");
                return null;
            }
        }

        public async Task<int> BuscarXidDocumento(int idDocumento, string nombreTipoDocumento)
        {
            var resultado = await _contexto.THistorialDocumentos
                .Where(a => a.IdDocumento == idDocumento && a.TipoDocumento == nombreTipoDocumento)
                .FirstOrDefaultAsync();
            if (resultado == null)
            {
                return 0;
            }
            int id = resultado.Id;
            if (id == 0)
            {
                return -1;
            }
            return id;
        }

    }
}
