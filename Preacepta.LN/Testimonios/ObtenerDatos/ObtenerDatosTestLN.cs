using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Preacepta.LN.Testimonios.ObtenerDatos
{
    public class ObtenerDatosTestLN : IObtenerDatosTestLN
    {
        /*metodo para usarlo en los vistas de detalles o deatails o buscar por id*/
        public TTestimonioDTO ObtenerDeDB(TTestimonio test)
        {
            return new TTestimonioDTO
            {
                IdTestimonio = test.IdTestimonio,
                IdCliente = test.IdCliente,
                Comentario = test.Comentario,
                Evaluacion = test.Evaluacion,
                Activo = test.Activo,
                IdClienteNavigation = test.IdClienteNavigation,
                Fecha = test.Fecha.ToString("dd/MM/yyyy"),
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TTestimonio ObtenerDeFront(TTestimonioDTO testDTO)
        {
            // DEBUG: Verifica el DTO recibido
            Console.WriteLine($"DTO recibido - Activo: {testDTO?.Activo}");

            var entidad = new TTestimonio
            {
                IdTestimonio = testDTO.IdTestimonio,
                IdCliente = testDTO.IdCliente,
                Comentario = testDTO.Comentario,
                Evaluacion = testDTO.Evaluacion,
                Activo = testDTO.Activo, // Forzado explícitamente
                IdClienteNavigation = testDTO.IdClienteNavigation,
                Fecha = DateTime.Now,
            };

            // DEBUG: Verifica la entidad creada
            Console.WriteLine($"Entidad creada - Activo: {entidad.Activo}");

            return entidad;
        }
    }
}
