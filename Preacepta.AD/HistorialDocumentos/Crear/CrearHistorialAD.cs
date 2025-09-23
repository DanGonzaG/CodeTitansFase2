using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.HistorialDocumentos.Crear
{
    public class CrearHistorialAD : ICrearHistorialAD
    {
        private readonly Contexto _contexto;

        public CrearHistorialAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(THistorialDocumento entidad)
        {
            if (entidad == null) return -1;

            entidad.TipoDocumento = (entidad.TipoDocumento ?? string.Empty).Trim();
            entidad.Titulo = (entidad.Titulo ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(entidad.TipoDocumento) || entidad.IdDocumento <= 0)
                return 0;

            if (string.IsNullOrWhiteSpace(entidad.Titulo))
                entidad.Titulo = $"{entidad.TipoDocumento} #{entidad.IdDocumento}";

            try
            {
                await _contexto.THistorialDocumentos.AddAsync(entidad);
                return await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CrearHistorialAD: {ex.Message}");
                return 0;
            }
        }
    }
}
