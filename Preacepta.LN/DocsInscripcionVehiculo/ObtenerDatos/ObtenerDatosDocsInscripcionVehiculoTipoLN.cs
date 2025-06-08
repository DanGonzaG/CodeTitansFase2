using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Preacepta.LN.DocsInscripcionVehiculo.ObtenerDatos
{
    public class ObtenerDatosDocsInscripcionVehiculoTipoLN : IObtenerDatosDocsInscripcionVehiculoTipoLN
    {
        public DocsInscripcionVehiculoDTO ObtenerDeDB(TDocsInscripcionVehiculo datos)
        {
            return new DocsInscripcionVehiculoDTO
            {
                Anio = datos.Anio,
                Capacidad = datos.Capacidad,
                Carroceria = datos.Carroceria,
                Categoria = datos.Categoria,
                CedulaAbogado = datos.CedulaAbogado,
                CedulaAbogadoNavigation = datos.CedulaAbogadoNavigation,
                CedulaCliente = datos.CedulaCliente,
                CedulaClienteNavigation = datos.CedulaClienteNavigation,
                Cilindraje = datos.Cilindraje,
                Color = datos.Color,
                Combustible = datos.Combustible,
                EstiloVehiculo = datos.EstiloVehiculo,
                EstiloVehiculoNavigation = datos.EstiloVehiculoNavigation,
                FechaFirma = datos.FechaFirma,
                IdDocumento = datos.IdDocumento,
                LugarFirma = datos.LugarFirma,
                LugarFirmaNavigation = datos.LugarFirmaNavigation,
                MarcaMotor = datos.MarcaMotor,
                MarcaVehiculo = datos.MarcaVehiculo,
                MarcaVehiculoNavigation = datos.MarcaVehiculoNavigation,
                ModeloVehiculo = datos.ModeloVehiculo,
                NumeroMotor = datos.NumeroMotor,
                NumeroSerieChasis = datos.NumeroSerieChasis,
                PesoBruto = datos.PesoBruto,
                PesoNeto = datos.PesoNeto,
                Potencia = datos.Potencia,
                Vin = datos.Vin
            };
        }


        /*metodo para obtner los datos de los formularios y pasarlos al modelo de acceso a datos*/
        public TDocsInscripcionVehiculo ObtenerDeFront(DocsInscripcionVehiculoDTO datos)
        {
            return new TDocsInscripcionVehiculo
            {
                Anio = datos.Anio,
                Capacidad = datos.Capacidad,
                Carroceria = datos.Carroceria,
                Categoria = datos.Categoria,
                CedulaAbogado = datos.CedulaAbogado,
                CedulaAbogadoNavigation = datos.CedulaAbogadoNavigation,
                CedulaCliente = datos.CedulaCliente,
                CedulaClienteNavigation = datos.CedulaClienteNavigation,
                Cilindraje = datos.Cilindraje,
                Color = datos.Color,
                Combustible = datos.Combustible,
                EstiloVehiculo = datos.EstiloVehiculo,
                EstiloVehiculoNavigation = datos.EstiloVehiculoNavigation,
                FechaFirma = datos.FechaFirma,
                IdDocumento = datos.IdDocumento,
                LugarFirma = datos.LugarFirma,
                LugarFirmaNavigation = datos.LugarFirmaNavigation,
                MarcaMotor = datos.MarcaMotor,
                MarcaVehiculo = datos.MarcaVehiculo,
                MarcaVehiculoNavigation = datos.MarcaVehiculoNavigation,
                ModeloVehiculo = datos.ModeloVehiculo,
                NumeroMotor = datos.NumeroMotor,
                NumeroSerieChasis = datos.NumeroSerieChasis,
                PesoBruto = datos.PesoBruto,
                PesoNeto = datos.PesoNeto,
                Potencia = datos.Potencia,
                Vin = datos.Vin
            };
        }
    }
}
