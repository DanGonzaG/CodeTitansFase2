using Preacepta.AD.DocsTipoVehiculo.Crear;
using Preacepta.LN.DocsTipoVehiculo.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.DocsTipoVehiculo.Crear
{
    public class CrearTipoVehiculoLN : ICrearTipoVehiculoLN
    {
        private readonly ICrearTipoVehiculoAD _crear;
        private readonly IObtenerDatosTipoVehiculoLN _obtenerDatos;

        public CrearTipoVehiculoLN(ICrearTipoVehiculoAD crear, IObtenerDatosTipoVehiculoLN obtenerDatos)
        {
            _crear = crear;
            _obtenerDatos = obtenerDatos;
        }

        public async Task<int> crear(DocsTipoVehiculoDTO tipovehiculoDTO)
        {
            if (tipovehiculoDTO == null)
            {
                Console.WriteLine("Error: Objeto nulo.");
                return 0;
            }
            try
            {
                int bandera = await _crear.crear(_obtenerDatos.ObtenerDeFront(tipovehiculoDTO));
                if (bandera == null)
                {
                    Console.WriteLine("Conversion fallida");
                    return 0;
                }
                return bandera;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearTipoVehiculoLN{ex.Message}");
                return -1;
            }
        }

    }
}
