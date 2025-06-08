using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.AD.GePersona.Listar
{
    public class ListarGePersonaAD : IListarGePersonaAD
    {
        private readonly Contexto _contexto;

        public ListarGePersonaAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<GePersonaDTO>> listar2()
        {
            List<GePersonaDTO> lista = await (from persona in _contexto.TGePersonas
                                              select new GePersonaDTO
                                              {
                                                  Cedula = persona.Cedula,
                                                  Nombre = persona.Nombre,
                                                  Apellido1 = persona.Apellido1,
                                                  Apellido2 = persona.Apellido2,
                                                  FechaNacimiento = persona.FechaNacimiento.ToString("dd/MM/yyyy"),
                                                  Edad = persona.Edad,
                                                  EstadoCivil = persona.EstadoCivil,
                                                  Oficio = persona.Oficio,
                                                  Direccion1 = persona.Direccion1,
                                                  Direccion2 = persona.Direccion2,
                                                  Email = persona.Email,
                                                  FechaRegistro = persona.FechaRegistro.ToString("dd/MM/yyyy"),
                                                  Activo = persona.Activo,
                                                  Telefono1 = persona.Telefono1,
                                                  Telefono2 = persona.Telefono2,
                                              }).ToListAsync();
            return lista;
        }

        public async Task<List<GePersonaDTO>> listar()
        {


            try
            {
                return await _contexto.TGePersonas
                    .Include(c => c.Direccion1Navigation)
                    .ThenInclude(a => a.IdCatonNavigation)
                    .Select(persona => new GePersonaDTO
                    {
                        Cedula = persona.Cedula,
                        Nombre = persona.Nombre,
                        Apellido1 = persona.Apellido1,
                        Apellido2 = persona.Apellido2,
                        FechaNacimiento = persona.FechaNacimiento.ToString("dd/MM/yyyy"),
                        Edad = persona.Edad,
                        EstadoCivil = persona.EstadoCivil,
                        Oficio = persona.Oficio,
                        Direccion1 = persona.Direccion1,
                        Direccion1Navigation = persona.Direccion1Navigation,
                        Direccion2 = persona.Direccion2,
                        Email = persona.Email,
                        FechaRegistro = persona.FechaRegistro.ToString("dd/MM/yyyy"),
                        Activo = persona.Activo,
                        Telefono1 = persona.Telefono1,
                        Telefono2 = persona.Telefono2,
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<GePersonaDTO>();
            }

        }
    }
}
