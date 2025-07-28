using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsContratoPrestacionServicios.BuscarXid
{
    public class BuscarDocsContratoPrestacionServiciosAD : IBuscarDocsContratoPrestacionServiciosAD
    {
        private readonly Contexto _contexto;

        public BuscarDocsContratoPrestacionServiciosAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TDocsContratoPrestacionServicio?> buscar(int id)
        {
            try
            {
                var lista = await _contexto.TDocsContratoPrestacionServicios
                .Include(t => t.CedulaAbogadoNavigation)
                .Include(t => t.CedulaClienteNavigation)
                .Include(t => t.CiudadFirmaNavigation)
                .Include(t => t.ProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.IdDocumento == id);
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarDocsContratoPrestacionServiciosAD, no se encontro id: {ex.Message}");
                return null;
            }

        }
    }
}
