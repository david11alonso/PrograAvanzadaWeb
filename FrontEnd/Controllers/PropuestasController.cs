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
    public class PropuestasController : BaseController
    {
        private readonly PrograAvanzadaWebContext _context;

        public PropuestasController(PrograAvanzadaWebContext context)
        {
            _context = context;
        }

        // GET: Propuestas
        public async Task<IActionResult> Index()
        {
            var prograAvanzadaWebContext = _context.Propuesta.Include(p => p.Usuario);
            return View(await prograAvanzadaWebContext.ToListAsync());
        }

        // GET: Propuestas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propuesta = await _context.Propuesta
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.PropuestaId == id);
            if (propuesta == null)
            {
                return NotFound();
            }

            return View(propuesta);
        }

        // GET: Propuestas/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "UserName");
            return View();
        }

        // POST: Propuestas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropuestaId,Titulo,Descripcion,Pendiente,Tipo,UsuarioId,FechaPublicacion,FechaFinalizacion,Problema,ResultadoEsperado,Riesgos,Beneficios")] Propuesta propuesta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propuesta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "UserName", propuesta.UsuarioId);
            return View(propuesta);
        }

        // GET: Propuestas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propuesta = await _context.Propuesta.FindAsync(id);
            if (propuesta == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "UserName", propuesta.UsuarioId);
            return View(propuesta);
        }

        // POST: Propuestas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PropuestaId,Titulo,Descripcion,Pendiente,Tipo,UsuarioId,FechaPublicacion,FechaFinalizacion,Problema,ResultadoEsperado,Riesgos,Beneficios")] Propuesta propuesta)
        {
            if (id != propuesta.PropuestaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propuesta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropuestaExists(propuesta.PropuestaId))
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
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "UserName", propuesta.UsuarioId);
            return View(propuesta);
        }

        // GET: Propuestas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {

                if (id == null)
                {
                    return NotFound();
                }

                var propuesta = await _context.Propuesta
                    .Include(p => p.Usuario)
                    .FirstOrDefaultAsync(m => m.PropuestaId == id);
                if (propuesta == null)
                {
                    return NotFound();
                }
                else
                {
                    var propuestaRemove = await _context.Propuesta.FindAsync(id);
                    _context.Propuesta.Remove(propuestaRemove);
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

        // POST: Propuestas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var propuesta = await _context.Propuesta.FindAsync(id);
        //    _context.Propuesta.Remove(propuesta);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool PropuestaExists(int id)
        {
            return _context.Propuesta.Any(e => e.PropuestaId == id);
        }
    }
}