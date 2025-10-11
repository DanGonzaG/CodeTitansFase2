using Microsoft.AspNetCore.Mvc;
using Preacepta.LN.BitacoraEventos.ObtenerDatos;
using Preacepta.Modelos.AbstraccionesFrond;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Preacepta.UI.Controllers
{
    public class BitacoraEventosController : Controller
    {
        private readonly IObtenerDatosEventosLN _obtenerDatosEventosLN;
        
        public BitacoraEventosController(IObtenerDatosEventosLN obtenerDatosLN)
        {
            _obtenerDatosEventosLN = obtenerDatosLN;
        }

        //GET: /BitacoraEventos
        public async Task<IActionResult> Index( string usuario, string tabla)
        {
            List<BitacoraEventosDTO> eventos;
            if (!string.IsNullOrWhiteSpace(usuario))
            {
                eventos = await _obtenerDatosEventosLN.ListarPorUsuario(usuario);
            }
            else if (!string.IsNullOrWhiteSpace(tabla))
            {
                eventos = await _obtenerDatosEventosLN.ObtenerEventosPorTabla(tabla);
        } else {
                eventos = await _obtenerDatosEventosLN.ListarTodos();
            }
            ViewData["Usuario"] = usuario;
            ViewData["Tabla"] = tabla;

            return View(eventos);

        }


        //GET: /BitacoraEventos/PorUsuario
        public async Task<IActionResult> PorUsuario(string usuario)
        {
            var eventos = await _obtenerDatosEventosLN.ListarPorUsuario(usuario);
            return View("Index", eventos);
        }

        //GET: /BitacoraEventos/PorTabla
        public async Task<IActionResult> PorTabla(string tabla)
        {
            var eventos = await _obtenerDatosEventosLN.ObtenerEventosPorTabla(tabla);
            return View("Index", eventos);
        }

        //GET: /BitacoraEventos/Detalle
        public async Task<IActionResult> Detalle(int id)
        {
            var evento = await _obtenerDatosEventosLN.ObtenerXid(id);
            if (evento == null) return NotFound();
            return View(evento);
        }
    }
}
