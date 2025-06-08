using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsContratoPrestacionServicios.Crear
{
    public class CrearDocsContratoPrestacionServiciosAD : ICrearDocsContratoPrestacionServiciosAD
    {
        private readonly Contexto _contexto;

        public CrearDocsContratoPrestacionServiciosAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crear(TDocsContratoPrestacionServicio crear)
        {
            if (crear == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TDocsContratoPrestacionServicios.AddAsync(crear);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearDocsContratoPrestacionServiciosAD {ex.Message}");
                return 0;
            }


        }
    }
}
