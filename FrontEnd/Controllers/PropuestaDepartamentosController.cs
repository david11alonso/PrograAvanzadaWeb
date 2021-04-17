using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrontEnd.Models;
using static FrontEnd.Enums.Enums;


namespace FrontEnd.Controllers
{
    public class PropuestaDepartamentosController : BaseController
    {
        private readonly PrograAvanzadaWebContext _context;

        public PropuestaDepartamentosController(PrograAvanzadaWebContext context)
        {
            _context = context;
        }

        // GET: PropuestaDepartamentoes
        public async Task<IActionResult> Index()
        {
            var prograAvanzadaWebContext = _context.PropuestaDepartamento.Include(p => p.Departamento).Include(p => p.Propuesta);
            return View(await prograAvanzadaWebContext.ToListAsync());
        }

        // GET: PropuestaDepartamentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propuestaDepartamento = await _context.PropuestaDepartamento
                .Include(p => p.Departamento)
                .Include(p => p.Propuesta)
                .FirstOrDefaultAsync(m => m.PropuestaDepartamentoId == id);
            if (propuestaDepartamento == null)
            {
                return NotFound();
            }

            return View(propuestaDepartamento);
        }

        // GET: PropuestaDepartamentoes/Create
        public IActionResult Create()
        {
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Nombre");
            ViewData["PropuestaId"] = new SelectList(_context.Propuesta, "PropuestaId", "Titulo");
            return View();
        }

        // POST: PropuestaDepartamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropuestaDepartamentoId,PropuestaId,DepartamentoId")] PropuestaDepartamento propuestaDepartamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propuestaDepartamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Nombre", propuestaDepartamento.DepartamentoId);
            ViewData["PropuestaId"] = new SelectList(_context.Propuesta, "PropuestaId", "Titulo", propuestaDepartamento.PropuestaId);
            return View(propuestaDepartamento);
        }

        // GET: PropuestaDepartamentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propuestaDepartamento = await _context.PropuestaDepartamento.FindAsync(id);
            if (propuestaDepartamento == null)
            {
                return NotFound();
            }
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Nombre", propuestaDepartamento.DepartamentoId);
            ViewData["PropuestaId"] = new SelectList(_context.Propuesta, "PropuestaId", "Titulo", propuestaDepartamento.PropuestaId);
            return View(propuestaDepartamento);
        }

        // POST: PropuestaDepartamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PropuestaDepartamentoId,PropuestaId,DepartamentoId")] PropuestaDepartamento propuestaDepartamento)
        {
            if (id != propuestaDepartamento.PropuestaDepartamentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propuestaDepartamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropuestaDepartamentoExists(propuestaDepartamento.PropuestaDepartamentoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Nombre", propuestaDepartamento.DepartamentoId);
            ViewData["PropuestaId"] = new SelectList(_context.Propuesta, "PropuestaId", "Titulo", propuestaDepartamento.PropuestaId);
            return View(propuestaDepartamento);
        }

        // GET: PropuestaDepartamentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {


                if (id == null)
                {
                    return NotFound();
                }

                var propuestaDepartamento = await _context.PropuestaDepartamento
                    .Include(p => p.Departamento)
                    .Include(p => p.Propuesta)
                    .FirstOrDefaultAsync(m => m.PropuestaDepartamentoId == id);
                if (propuestaDepartamento == null)
                {
                    return NotFound();
                }
                else
                {
                    var propuestaDepartamentoRemove = await _context.PropuestaDepartamento.FindAsync(id);
                    _context.PropuestaDepartamento.Remove(propuestaDepartamentoRemove);
                    await _context.SaveChangesAsync();
                    NotifyDelete("El registro se ha eliminado correctamente");
                }


            }
            catch (Exception)
            {

                NotifyError("El registro no puede ser eliminado. Contacte a su administrador", notificationType: NotificationType.error);
            }


            return RedirectToAction(nameof(Index));
        }

        // POST: PropuestaDepartamentoes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var propuestaDepartamento = await _context.PropuestaDepartamento.FindAsync(id);
        //    _context.PropuestaDepartamento.Remove(propuestaDepartamento);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool PropuestaDepartamentoExists(int id)
        {
            return _context.PropuestaDepartamento.Any(e => e.PropuestaDepartamentoId == id);
        }
    }
}
