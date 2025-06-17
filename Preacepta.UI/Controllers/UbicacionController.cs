using Microsoft.AspNetCore.Mvc;
using Preacepta.LN.CrDireccion1.Listar;

namespace Preacepta.UI.Controllers
{
    [Route("Ubicacion")]
    public class UbicacionController : Controller
    {
        private readonly IListarCrDireccion1LN _listarDireccion;

        public UbicacionController(IListarCrDireccion1LN listarDireccion) 
        {
            _listarDireccion = listarDireccion;
        }

        [HttpGet("Provincias")]
        public async Task<IActionResult> ObtenerProvincias()
        {
            var provincias = await _listarDireccion.listarProvincias();
            return Ok(provincias);
        }

        [HttpGet("Cantones/{IdProvincia}")]
        public async Task<IActionResult> ObtenerCantones(int IdProvincia)
        {
            var cantones = await _listarDireccion.listarCantonesXprovincia(IdProvincia);

            return Ok(cantones);
        }

        [HttpGet("Distritos/{cantonId}")]
        public async Task<IActionResult> ObtenerDistritos(int cantonId)
        {
            var distritos = await _listarDireccion.listarDistritosXCanton(cantonId);
            return Ok(distritos);
        }
    }
}
