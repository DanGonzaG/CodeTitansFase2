using Microsoft.EntityFrameworkCore;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Preacepta.AD.Testimonios.Listar
{
    public class ListarTestAD : IListarTestAD
    {
        private readonly Contexto _contexto;
        public ListarTestAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        /*Nueva prueba*/
        public async Task<List<TTestimonioDTO>> listar2()
        {
            List<TTestimonioDTO> lista = await (from test in _contexto.TTestimonios
                                              select new TTestimonioDTO
                                              {
                                                  IdTestimonio = test.IdTestimonio,
                                                  Fecha = test.Fecha.ToString("yyyy-MM-dd HH:mm"),
                                                  IdCliente = test.IdCliente,
                                                  Comentario = test.Comentario,
                                                  Evaluacion = test.Evaluacion,
                                                  Activo = test.Activo,
                                                  IdClienteNavigation = test.IdClienteNavigation
                                              }).ToListAsync();
            return lista;
        }

        public async Task<List<TTestimonioDTO>> Listar()
        {


            try
            {
                return await _contexto.TTestimonios
            .Include(t => t.IdClienteNavigation) // Incluye la relación con Persona
            .Where(t => t.Activo)
            .Select(test => new TTestimonioDTO
            {
                IdTestimonio = test.IdTestimonio,
                Fecha = test.Fecha.ToString("yyyy-MM-dd HH:mm"),
                IdCliente = test.IdCliente,
                Comentario = test.Comentario,
                Evaluacion = test.Evaluacion,
                Activo = test.Activo,
                IdClienteNavigation = test.IdClienteNavigation != null ? new TGePersona
                {
                    Nombre = test.IdClienteNavigation.Nombre,
                    Apellido1 = test.IdClienteNavigation.Apellido1,
                    Apellido2 = test.IdClienteNavigation.Apellido2,
                    Cedula = test.IdClienteNavigation.Cedula
                } : null
            }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos {ex.Message}");
                return new List<TTestimonioDTO>();
            }




            /*Original Listar*/
            /*
            public async Task<List<TTestimonioDTO>> Listar()
        {
            try
            {
                return await _contexto.TTestimonios
                    .Include(t => t.IdClienteNavigation)
                    .Select(testi => new TTestimonioDTO
                            {
                        IdTestimonio = testi.IdTestimonio,
                        Fecha = testi.Fecha.ToString("yyyy-MM-dd HH:mm"),
                        IdCliente = testi.IdCliente,
                        Comentario = testi.Comentario,
                        Evaluacion = testi.Evaluacion,
                        Activo = testi.Activo,
                        IdClienteNavigation = testi.IdClienteNavigation != null
                ? new TGePersona
                {
                    Cedula = testi.IdClienteNavigation.Cedula,
                    Nombre = testi.IdClienteNavigation.Nombre,
                    Apellido1 = testi.IdClienteNavigation.Apellido1,
                    Apellido2 = testi.IdClienteNavigation.Apellido2
                }
                : null
                            }).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener testimonios: {ex.Message}");
                return new List<TTestimonioDTO>();
            }*/
        }
    }
}
