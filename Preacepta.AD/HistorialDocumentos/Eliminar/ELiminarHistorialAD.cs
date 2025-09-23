using Microsoft.EntityFrameworkCore;
using Preacepta.AD.HistorialDocumentos.BuscarXid;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.HistorialDocumentos.Eliminar
{
    public class ELiminarHistorialAD : IELiminarHistorialAD
    {
        private readonly Contexto _contexto;
        private readonly IBuscarHistorialAD _buscarXid;

        public ELiminarHistorialAD(Contexto contexto, IBuscarHistorialAD buscarXid)
        {
            _contexto = contexto;
            _buscarXid = buscarXid;
        }

        public async Task<int> Eliminar(int id)
        {
            try
            {
                THistorialDocumento? encontrado = await _buscarXid.Buscar(id);
                if (encontrado is null)
                {
                    Console.WriteLine("EliminarHistorialAD: no se encontró el id.");
                    return 0;
                }

                // Si hay una instancia local, la despegamos para evitar conflictos
                var local = _contexto.THistorialDocumentos.Local.FirstOrDefault(x => x.Id == id);
                if (local != null)
                    _contexto.Entry(local).State = EntityState.Detached;

                // Quitar (EF adjunta el entity si viene AsNoTracking)
                _contexto.THistorialDocumentos.Remove(encontrado);

                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"EliminarHistorialAD DbUpdateException: {ex.Message}");
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EliminarHistorialAD Error: {ex.Message}");
                return -1;
            }
        }
    }
}
