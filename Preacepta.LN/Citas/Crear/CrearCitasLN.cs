using Preacepta.AD.Citas.Crear;
using Preacepta.LN.Citas.ObtenerDatos;
using Preacepta.LN.CitasTipo.Listar;
using Preacepta.LN.GeAbogado.BuscarXid;
using Preacepta.LN.GePersona.BuscarXid;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.LN.Citas.Crear
{
    public class CrearCitasLN : ICrearCitasLN
    {
        private readonly ICrearCitasAD _crear;
        private readonly IObtenerDatosCitasLN _obtenerDatosLN;
        private readonly IListarCitasTipoLN _listarCitasTipoLN;
        private readonly IBuscarAbogadoLN _buscarAbogadoLN;
        private readonly IBuscarXidGePersonaLN _buscarPersonaLN;

        public CrearCitasLN(
            ICrearCitasAD crearCitasAD,
            IObtenerDatosCitasLN obtenerDatosLN,
            IListarCitasTipoLN listarCitasTipoLN,
            IBuscarAbogadoLN buscarAbogadoLN,
            IBuscarXidGePersonaLN buscarPersonaLN)
        {
            _crearCitasAD = crearCitasAD ?? throw new ArgumentNullException(nameof(crearCitasAD));
            _obtenerDatosLN = obtenerDatosLN ?? throw new ArgumentNullException(nameof(obtenerDatosLN));
            _listarCitasTipoLN = listarCitasTipoLN ?? throw new ArgumentNullException(nameof(listarCitasTipoLN));
            _buscarAbogadoLN = buscarAbogadoLN ?? throw new ArgumentNullException(nameof(buscarAbogadoLN));
            _buscarPersonaLN = buscarPersonaLN ?? throw new ArgumentNullException(nameof(buscarPersonaLN));
        }

        public async Task<int> crear(CitasDTO dto)
        {
            if (dto == null)
            {
                Console.WriteLine("Error: Objeto nulo.");
                return 0;
            }

            
            if (dto.Fecha < DateOnly.FromDateTime(DateTime.Now) ||
                (dto.Fecha == DateOnly.FromDateTime(DateTime.Now) && dto.Hora.ToTimeSpan() < DateTime.Now.TimeOfDay))
            {
                Console.WriteLine("La fecha y hora deben ser futuras.");
                return 0;
            }

          
            var tipos = await _listarCitasTipoLN.listar();
            if (!tipos.Any(t => t.Id == dto.IdTipoCita))
            {
                Console.WriteLine("El tipo de cita seleccionado no es válido.");
                return 0;
            }

       
            var abogado = await _buscarAbogadoLN.buscar(dto.Anfitrion);
            if (abogado == null)
            {
                Console.WriteLine("El abogado indicado no existe en la base de datos.");
                return 0;
            }

           
            if (dto.IdCliente == null)
            {
                Console.WriteLine("Debe seleccionar un cliente válido.");
                return 0;
            }

            var cliente = await _buscarPersonaLN.buscar(dto.IdCliente.Value);
            if (cliente == null)
            {
                Console.WriteLine("El cliente no existe en la base de datos.");
                return 0;
            }

           
            try
            {
               
                int idCita = await _crearCitasAD.crear(_obtenerDatosLN.ObtenerDeFront(dto));
                if (idCita <= 0) return 0;

                bool asignado = await _crearCitasAD.AsignarClienteAlaCita(idCita, dto.IdCliente.Value);
                if (!asignado) return 0;

                return idCita;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearCitasLN.crear: {ex.Message}");
                return -1;
            }
        }

        private readonly ICrearCitasAD _crearCitasAD;

        public async Task<bool> AsignarClienteAlaCita(int idCita, int idCliente)
        {
          
            return await _crearCitasAD.AsignarClienteAlaCita(idCita, idCliente);
        }

    }
}
