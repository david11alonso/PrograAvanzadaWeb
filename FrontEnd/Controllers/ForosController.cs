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
    public class ForosController : BaseController
    {
        private readonly PrograAvanzadaWebContext _context;

        public ForosController(PrograAvanzadaWebContext context)
        {
            _context = context;
        }

        // GET: Foroes
        public async Task<IActionResult> Index()
        {
            var prograAvanzadaWebContext = _context.Foro.Include(f => f.Propuesta);
            return View(await prograAvanzadaWebContext.ToListAsync());
        }

        // GET: Foroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foro = await _context.Foro
                .Include(f => f.Propuesta)
                .FirstOrDefaultAsync(m => m.ForoId == id);
            if (foro == null)
            {
                return NotFound();
            }

            return View(foro);
        }

        // GET: Foroes/Create
        public IActionResult Create()
        {
            ViewData["PropuestaId"] = new SelectList(_context.Propuesta, "PropuestaId", "Titulo");
            return View();
        }

        // POST: Foroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ForoId,PropuestaId")] Foro foro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PropuestaId"] = new SelectList(_context.Propuesta, "PropuestaId", "Titulo", foro.PropuestaId);
            return View(foro);
        }

        // GET: Foroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foro = await _context.Foro.FindAsync(id);
            if (foro == null)
            {
                return NotFound();
            }
            ViewData["PropuestaId"] = new SelectList(_context.Propuesta, "PropuestaId", "Titulo", foro.PropuestaId);
            return View(foro);
        }

        // POST: Foroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ForoId,PropuestaId")] Foro foro)
        {
            if (id != foro.ForoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForoExists(foro.ForoId))
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
            ViewData["PropuestaId"] = new SelectList(_context.Propuesta, "PropuestaId", "Titulo", foro.PropuestaId);
            return View(foro);
        }

        // GET: Foroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var foro = await _context.Foro
                    .Include(f => f.Propuesta)
                    .FirstOrDefaultAsync(m => m.ForoId == id);
                if (foro == null)
                {
                    return NotFound();
                }
                else
                {
                    var foroRemove = await _context.Foro.FindAsync(id);
                    _context.Foro.Remove(foroRemove);
                    await _context.SaveChangesAsync();
                    //try save data into database
                    NotifyDelete("El registro se ha eliminado correctamente");
                }


            }
            catch (Exception)
            {

                NotifyError("El registro no puede ser eliminado. Contacte a su administrador", notificationType: NotificationType.error);
            }

            return RedirectToAction(nameof(Index));
        }

        //// POST: Foroes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var foro = await _context.Foro.FindAsync(id);
        //    _context.Foro.Remove(foro);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ForoExists(int id)
        {
            return _context.Foro.Any(e => e.ForoId == id);
        }
    }
}
