
using Preacepta.AD;
using Preacepta.AD.Citas.BuscarXid;
using Preacepta.LN.Citas.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Citas.BuscarXid
{
    public class BuscarCitasLN : IBuscarCitasLN
    {
        private readonly IBuscarCitasAD _buscar;
        private readonly IObtenerDatosCitasLN _obtenerDatosLN;

        public BuscarCitasLN(IBuscarCitasAD buscar,
             IObtenerDatosCitasLN obtnerDatosLN)
        {
            _buscar = buscar;
            _obtenerDatosLN = obtnerDatosLN;
        }

        public async Task<CitasDTO?> buscar(int id)
        {
            try
            {
                TCita? resultadoBusqueda = await _buscar.buscar(id);
                if (resultadoBusqueda == null)
                {
                    Console.WriteLine("No se encontró la cita.");
                    return null;
                }
                CitasDTO obtenerDatos = _obtenerDatosLN.ObtenerDeDB(resultadoBusqueda);
                return obtenerDatos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en BuscarCitasLN: {ex.Message}");
                return null;
            }
        }
        public async Task<List<CitasDTO>> obtenerTodas()
            {
                try
                {
                    var resultados = await _buscar.obtenerTodas(); 
                    if (resultados == null || !resultados.Any())
                        return new List<CitasDTO>();

                   
                    List<CitasDTO> citasDto = resultados.Select(cita => _obtenerDatosLN.ObtenerDeDB(cita)).ToList();

                    return citasDto;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en BuscarCitasLN.obtenerTodas: {ex.Message}");
                    return new List<CitasDTO>();
                }
            }


        public async Task<CitasDTO?> ObtenerCitaConClientes(int idCita)
        {
           
            return await _buscar.ObtenerCitaConClientes(idCita);
        }
        public async Task<List<CitasDTO>> ListarPorIdCliente(int idCliente)
        {
            var citas = await _buscar.ListarPorIdCliente(idCliente); 
            return citas.Select(c => _obtenerDatosLN.ObtenerDeDB(c)).ToList();
        }

        public async Task<CitasDTO?> CambiarEstadoYObtenerDatosAsync(int idCita, int nuevoEstado)
        {
            var cita = await _buscar.CambiarEstadoYObtenerDatosAsync(idCita, nuevoEstado);
            if (cita == null) return null;
            return _obtenerDatosLN.ObtenerDeDB(cita);
        }

        public async Task<CitasDTO?> ObtenerCitaConDocumentosAsync(int idCita)
        {
            return await _obtenerDatosLN.ObtenerCitaConDocumentosAsync(idCita);
        }


    }

}
