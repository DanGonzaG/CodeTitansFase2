using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preacepta.AD;
using Preacepta.LN.DocsContratoPrestacionServicios.BuscarXid;
using Preacepta.LN.DocsContratoPrestacionServicios.Crear;
using Preacepta.LN.DocsContratoPrestacionServicios.Editar;
using Preacepta.LN.DocsContratoPrestacionServicios.Eliminar;
using Preacepta.LN.DocsContratoPrestacionServicios.Listar;
using Preacepta.Modelos.AbstraccionesBD;
using Preacepta.Modelos.AbstraccionesFrond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Preacepta.UI.Controllers
{
    public class DocsContratoPrestacionServiciosController : Controller
    {
        private readonly Contexto _context;
        private readonly IBuscarDocsContratoPrestacionServiciosLN _buscar;
        private readonly ICrearDocsContratoPrestacionServiciosLN _crear;
        private readonly IEditarDocsContratoPrestacionServiciosLN _editar;
        private readonly IEliminarDocsContratoPrestacionServiciosLN _eliminar;
        private readonly IListarDocsContratoPrestacionServiciosLN _listar;


        public DocsContratoPrestacionServiciosController(Contexto context,
            IBuscarDocsContratoPrestacionServiciosLN buscar,
            ICrearDocsContratoPrestacionServiciosLN crear,
            IEditarDocsContratoPrestacionServiciosLN editar,
            IEliminarDocsContratoPrestacionServiciosLN eliminar,
            IListarDocsContratoPrestacionServiciosLN listar)
        {
            _context = context;
            _buscar = buscar;
            _crear = crear;
            _editar = editar;
            _eliminar = eliminar;
            _listar = listar;

        }

        // GET: ContratoPrestacionServicios
        public async Task<IActionResult> Index()
        {
            //var contexto = _context.TDocsContratoPrestacionServicios.Include(t => t.CedulaAbogadoNavigation).Include(t => t.CedulaClienteNavigation).Include(t => t.CiudadFirmaNavigation).Include(t => t.ProvinciaNavigation);
            return View(await _listar.listar());
        }

        // GET: ContratoPrestacionServicios/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsContratoPrestacionServicio = await _buscar.buscar(id);
            if (tDocsContratoPrestacionServicio == null)
            {
                return NotFound();
            }

            return View(tDocsContratoPrestacionServicio);
        }

        // GET: ContratoPrestacionServicios/Create
        public IActionResult Create()
        {
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula");
            ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1");
            ViewData["CiudadFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito");
            ViewData["Provincia"] = new SelectList(_context.TCrProvincias, "IdProvincia", "NombreProvincia");
            return View();
        }

        // POST: ContratoPrestacionServicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDocumento,RazonSocialEmpresa,Provincia,CedulaJuridicaEmpresa,CedulaAbogado,CedulaCliente,TipoServicios,FechaInicio,FechaFinal,MontoHonorarios,InformacionConfidencial,CiudadFirma,HoraFirma,FechaFirma")] DocsContratoPrestacionServicioDTO tDocsContratoPrestacionServicio)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tDocsContratoPrestacionServicio);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsContratoPrestacionServicio.CedulaAbogado);
            ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsContratoPrestacionServicio.CedulaCliente);
            ViewData["CiudadFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsContratoPrestacionServicio.CiudadFirma);
            ViewData["Provincia"] = new SelectList(_context.TCrProvincias, "IdProvincia", "IdProvincia", tDocsContratoPrestacionServicio.Provincia);
            return View(tDocsContratoPrestacionServicio);
        }

        // GET: ContratoPrestacionServicios/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsContratoPrestacionServicio = await _buscar.buscar(id);
            if (tDocsContratoPrestacionServicio == null)
            {
                return NotFound();
            }
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsContratoPrestacionServicio.CedulaAbogado);
            ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsContratoPrestacionServicio.CedulaCliente);
            ViewData["CiudadFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsContratoPrestacionServicio.CiudadFirma);
            ViewData["Provincia"] = new SelectList(_context.TCrProvincias, "IdProvincia", "IdProvincia", tDocsContratoPrestacionServicio.Provincia);
            return View(tDocsContratoPrestacionServicio);
        }

        // POST: ContratoPrestacionServicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDocumento,RazonSocialEmpresa,Provincia,CedulaJuridicaEmpresa,CedulaAbogado,CedulaCliente,TipoServicios,FechaInicio,FechaFinal,MontoHonorarios,InformacionConfidencial,CiudadFirma,HoraFirma,FechaFirma")] DocsContratoPrestacionServicioDTO tDocsContratoPrestacionServicio)
        {
            if (id != tDocsContratoPrestacionServicio.IdDocumento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _editar.Editar(tDocsContratoPrestacionServicio);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsContratoPrestacionServicio.CedulaAbogado);
            ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsContratoPrestacionServicio.CedulaCliente);
            ViewData["CiudadFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsContratoPrestacionServicio.CiudadFirma);
            ViewData["Provincia"] = new SelectList(_context.TCrProvincias, "IdProvincia", "IdProvincia", tDocsContratoPrestacionServicio.Provincia);
            return View(tDocsContratoPrestacionServicio);
        }

        // GET: ContratoPrestacionServicios/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDocsContratoPrestacionServicio = await _buscar.buscar(id);
            if (tDocsContratoPrestacionServicio == null)
            {
                return NotFound();
            }

            return View(tDocsContratoPrestacionServicio);
        }

        // POST: ContratoPrestacionServicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eliminar.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        //DE AQUI EN ADELNATE ESTAN MIS METODOS
        // GET: ContratoPrestacionServicios/Create
        public IActionResult CreateDocsContratoPrestacionServicios()
        {
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula");
            ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1");
            ViewData["CiudadFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito");
            ViewData["Provincia"] = new SelectList(_context.TCrProvincias, "IdProvincia", "NombreProvincia");
            return View();
        }

        // POST: ContratoPrestacionServicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDocsContratoPrestacionServicios([Bind("IdDocumento,RazonSocialEmpresa,Provincia,CedulaJuridicaEmpresa,CedulaAbogado,CedulaCliente,TipoServicios,FechaInicio,FechaFinal,MontoHonorarios,InformacionConfidencial,CiudadFirma,HoraFirma,FechaFirma")] DocsContratoPrestacionServicioDTO tDocsContratoPrestacionServicio)
        {
            if (ModelState.IsValid)
            {
                await _crear.Crear(tDocsContratoPrestacionServicio);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CedulaAbogado"] = new SelectList(_context.TGeAbogados, "Cedula", "Cedula", tDocsContratoPrestacionServicio.CedulaAbogado);
            ViewData["CedulaCliente"] = new SelectList(_context.TGePersonas, "Cedula", "Apellido1", tDocsContratoPrestacionServicio.CedulaCliente);
            ViewData["CiudadFirma"] = new SelectList(_context.TCrDistritos, "IdDistrito", "NombreDistrito", tDocsContratoPrestacionServicio.CiudadFirma);
            ViewData["Provincia"] = new SelectList(_context.TCrProvincias, "IdProvincia", "IdProvincia", tDocsContratoPrestacionServicio.Provincia);
            return View(tDocsContratoPrestacionServicio);
        }
    }
}
