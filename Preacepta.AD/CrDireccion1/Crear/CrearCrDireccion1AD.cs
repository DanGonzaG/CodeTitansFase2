using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD.CrDireccion1.Crear
{
    public class CrearCrDireccion1AD : ICrearCrDireccion1AD
    {
        private readonly Contexto _contexto;

        public CrearCrDireccion1AD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> crearProvincia(TCrProvincia crear)
        {
            if (crear == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TCrProvincias.AddAsync(crear);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearCrDireccion1AD-crear-TCrProvincia {ex.Message}");
                return 0;
            }
        }

        public async Task<int> crearCanton(TCrCantone crear)
        {
            if (crear == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TCrCantones.AddAsync(crear);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearCrDireccion1AD-crear-TCrCantone {ex.Message}");
                return 0;
            }
        }

        public async Task<int> crearDistrito(TCrDistrito crear)
        {
            if (crear == null)
            {
                Console.WriteLine("El objeto recibo fue nulo");
                return -1;
            }
            try
            {
                await _contexto.TCrDistritos.AddAsync(crear);
                int guardado = await _contexto.SaveChangesAsync();
                return guardado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearCrDireccion1AD-crear-TCrDistrito {ex.Message}");
                return 0;
            }
        }
    }
}
