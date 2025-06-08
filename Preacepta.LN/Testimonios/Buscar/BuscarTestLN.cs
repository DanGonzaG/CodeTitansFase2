using Preacepta.AD.Testimonios.Buscar;
using Preacepta.LN.GePersona.ObtenerDatos;
using Preacepta.LN.Testimonios.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Testimonios.Buscar
{
    public class BuscarTestLN : IBuscarTestLN
    {
        private readonly IBuscarTestAD _buscarTestAD;
        private readonly IObtenerDatosTestLN _obtenerDatosTestLN;

        public BuscarTestLN(IBuscarTestAD buscarTestAD,
            IObtenerDatosTestLN obtenerDatosTestLN)
        {
            _buscarTestAD = buscarTestAD;
            _obtenerDatosTestLN = obtenerDatosTestLN;
        }

        public async Task<TTestimonioDTO?> buscar(int id)
        {
            try
            {
                // Cambiar tipo aquí
                TTestimonio? resultadoBusqueda = await _buscarTestAD.buscar(id);

                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró el tipo de testimonio.");
                    return null;
                }
                TTestimonioDTO obtenerDatos = _obtenerDatosTestLN.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarTestimonioLN: {ex.Message}");
                return null;
            }
        }

        public async Task<GePersonaDTO?> BuscarClientePorId(int idCliente)
        {
            TTestimonio? resultadoBusqueda = await _buscarTestAD.buscar(idCliente);

            var cliente = await _buscarTestAD.BuscarPorId(idCliente);

            if (cliente == null) return null;

            return new GePersonaDTO
            {
                Cedula = cliente.Cedula,
                Nombre = cliente.Nombre,
                // Mapea otros campos si es necesario
            };
        }

    }
}
