using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.LN.Casos.BuscarXid;
using Preacepta.LN.Casos.Crear;
using Preacepta.LN.Casos.Editar;
using Preacepta.LN.Casos.Eliminar;
using Preacepta.LN.Casos.Listar;
using Preacepta.LN.CasosTipo.Listar;
using Preacepta.LN.GeAbogado.Listar;
using Preacepta.LN.GePersona.Listar;
using Preacepta.Modelos.AbstraccionesFrond;

namespace Preacepta.UI.Controllers
{
    public class CasoController : Controller
    {
        private readonly IBuscarCasosLN _buscar;
        private readonly ICrearCasosLN _crear;
        private readonly IEditarCasosLN _editar;
        private readonly IELiminarCasosLN _eLiminar;
        private readonly IListarCasosLN _listar;

        private readonly IListarAbogadoLN _listarAbogados;
        private readonly IListarGePersonaLN _listarGePersona;
        private readonly IListarCasosTipoLN _listarCasosTipoLN;

        public CasoController(
            IBuscarCasosLN buscar,
            ICrearCasosLN crear,
            IEditarCasosLN editar,
            IELiminarCasosLN eLiminar,
            IListarCasosLN listar,
            IListarAbogadoLN listarAbogados,
            IListarGePersonaLN listarGePersona,
            IListarCasosTipoLN listarCasosTipoLN)
        {
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _listar = listar;
            _listarAbogados = listarAbogados;
            _listarGePersona = listarGePersona;
            _listarCasosTipoLN = listarCasosTipoLN;
        }

        // GET: Caso
        public async Task<IActionResult> Index()
        {
            //var contexto = _context.TCasos.Include(t => t.IdAbogadoNavigation).Include(t => t.IdClienteNavigation).Include(t => t.IdTipoCasoNavigation);
            return View(await _listar.listar());
        }

        // GET: Caso/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCaso = await _buscar.buscar(id);
            if (tCaso == null)
            {
                return NotFound();
            }

            return View(tCaso);
        }

        // GET: Caso/Create
        public async Task<IActionResult> Create()
        {
            //ViewData["IdAbogado"] = new SelectList(_listarAbogados.listar().Result, "Cedula", "CedulaNavigation.Nombre");
            //ViewData["IdCliente"] = new SelectList(_listarGePersona.listar().Result, "Cedula", "Nombre");

            ViewData["IdTipoCaso"] = new SelectList(_listarCasosTipoLN.listar().Result, "IdTipoCaso", "Nombre");

            ViewData["IdAbogado"] = (await _listarAbogados.listar())
                .Select(n => new SelectListItem
                {
                    Value = n.Cedula.ToString(),
                    Text = $"{n.Carnet} - Abogado(a) {n.CedulaNavigation.Nombre}"
                })
                .ToList();

            ViewData["IdCliente"] = (await _listarGePersona.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Cedula} - {n.Nombre} {n.Apellido1} {n.Apellido2}"
               })
               .ToList();
            return View();
        }

        // POST: Caso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCaso,Nombre,Fecha,IdTipoCaso,Descripcion,IdAbogado,IdCliente,Activo")] CasoDTO tCaso)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tCaso);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoCaso"] = new SelectList(_listarCasosTipoLN.listar().Result, "IdTipoCaso", "Nombre");

            ViewData["IdAbogado"] = (await _listarAbogados.listar())
                .Select(n => new SelectListItem
                {
                    Value = n.Cedula.ToString(),
                    Text = $"{n.Carnet} - Abogado(a) {n.CedulaNavigation.Nombre}"
                })
                .ToList();

            ViewData["IdCliente"] = (await _listarGePersona.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Cedula} - {n.Nombre} {n.Apellido1} {n.Apellido2}"
               })
               .ToList();
            return View(tCaso);
        }

        // GET: Caso/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCaso = await _buscar.buscar(id);
            if (tCaso == null)
            {
                return NotFound();
            }
            ViewData["IdTipoCaso"] = new SelectList(_listarCasosTipoLN.listar().Result, "IdTipoCaso", "Nombre");

            ViewData["IdAbogado"] = (await _listarAbogados.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Carnet} - Abogado(a) {n.CedulaNavigation.Nombre}"
               })
               .ToList();

            ViewData["IdCliente"] = (await _listarGePersona.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Cedula} - {n.Nombre} {n.Apellido1} {n.Apellido2}"
               })
               .ToList();
            return View(tCaso);
        }

        // POST: Caso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCaso,Nombre,Fecha,IdTipoCaso,Descripcion,IdAbogado,IdCliente,Activo")] CasoDTO tCaso)
        {
            if (id != tCaso.IdCaso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.Editar(tCaso);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();

                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoCaso"] = new SelectList(_listarCasosTipoLN.listar().Result, "IdTipoCaso", "Nombre");

            ViewData["IdAbogado"] = (await _listarAbogados.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Carnet} - Abogado(a) {n.CedulaNavigation.Nombre}"
               })
               .ToList();

            ViewData["IdCliente"] = (await _listarGePersona.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Cedula} - {n.Nombre} {n.Apellido1} {n.Apellido2}"
               })
               .ToList();
            return View(tCaso);
        }

        // GET: Caso/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCaso = await _buscar.buscar(id);
            if (tCaso == null)
            {
                return NotFound();
            }

            return View(tCaso);
        }

        // POST: Caso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID no válido.");
            }
            try
            {
                await _eLiminar.Eliminar(id);
                return RedirectToAction(nameof(Index));
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Error de referencia nula: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Otro error: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }


        }


        // GET: Caso/Create
        public async Task<IActionResult> FormularioCaso()
        {
            ViewData["IdTipoCaso"] = new SelectList(_listarCasosTipoLN.listar().Result, "IdTipoCaso", "Nombre");

            ViewData["IdAbogado"] = (await _listarAbogados.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Carnet} - Abogado(a) {n.CedulaNavigation.Nombre}"
               })
               .ToList();

            ViewData["IdCliente"] = (await _listarGePersona.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Cedula} - {n.Nombre} {n.Apellido1} {n.Apellido2}"
               })
               .ToList();
            return View();
        }

        // POST: Caso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FormularioCaso([Bind("Nombre,IdTipoCaso,Descripcion,IdAbogado,IdCliente")] CasoDTO tCaso)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tCaso);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoCaso"] = new SelectList(_listarCasosTipoLN.listar().Result, "IdTipoCaso", "Nombre");

            ViewData["IdAbogado"] = (await _listarAbogados.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Carnet} - Abogado(a) {n.CedulaNavigation.Nombre}"
               })
               .ToList();

            ViewData["IdCliente"] = (await _listarGePersona.listar())
               .Select(n => new SelectListItem
               {
                   Value = n.Cedula.ToString(),
                   Text = $"{n.Cedula} - {n.Nombre} {n.Apellido1} {n.Apellido2}"
               })
               .ToList();
            return View(tCaso);
        }


        // GET: ListarCasos
        public async Task<IActionResult> CasosListado()
        {
            //var contexto = _context.TCasos.Include(t => t.IdAbogadoNavigation).Include(t => t.IdClienteNavigation).Include(t => t.IdTipoCasoNavigation);
            return View(await _listar.listar());
        }

        // GET: Caso/Details/5
        public async Task<IActionResult> EtapasPL(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCaso = await _buscar.buscar(id);
            if (tCaso == null)
            {
                return NotFound();
            }

            return View(tCaso);
        }
    }
}
