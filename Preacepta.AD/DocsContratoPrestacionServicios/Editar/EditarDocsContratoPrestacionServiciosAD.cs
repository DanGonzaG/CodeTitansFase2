using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.DocsContratoPrestacionServicios.Editar
{
    public class EditarDocsContratoPrestacionServiciosAD : IEditarDocsContratoPrestacionServiciosAD
    {
        private readonly Contexto _contexto;
        public EditarDocsContratoPrestacionServiciosAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> Editar(TDocsContratoPrestacionServicio editar)
        {
            if (editar == null)
            {
                return 0;
            }

            try
            {
                _contexto.TDocsContratoPrestacionServicios.Update(editar);
                int bandera = await _contexto.SaveChangesAsync();
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarDocsContratoPrestacionServiciosAD : {ex.Message}");
                return -1;
            }

        }
    }
}
