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
    public class VotoPropuestasController : BaseController
    {
        private readonly PrograAvanzadaWebContext _context;

        public VotoPropuestasController(PrograAvanzadaWebContext context)
        {
            _context = context;
        }

        // GET: VotoPropuestas
        public async Task<IActionResult> Index()
        {
            var prograAvanzadaWebContext = _context.VotoPropuesta.Include(v => v.Propuesta).Include(v => v.Usuario);
            return View(await prograAvanzadaWebContext.ToListAsync());
        }

        // GET: VotoPropuestas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votoPropuesta = await _context.VotoPropuesta
                .Include(v => v.Propuesta)
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(m => m.VotoPropuestaId == id);
            if (votoPropuesta == null)
            {
                return NotFound();
            }

            return View(votoPropuesta);
        }

        // GET: VotoPropuestas/Create
        public IActionResult Create()
        {
            ViewData["PropuestaId"] = new SelectList(_context.Propuesta, "PropuestaId", "Titulo");
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "UserName");
            return View();
        }

        // POST: VotoPropuestas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VotoPropuestaId,PropuestaId,Votacion,UsuarioId,Comentario")] VotoPropuesta votoPropuesta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(votoPropuesta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PropuestaId"] = new SelectList(_context.Propuesta, "PropuestaId", "Titulo", votoPropuesta.PropuestaId);
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "UserName", votoPropuesta.UsuarioId);
            return View(votoPropuesta);
        }

        // GET: VotoPropuestas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votoPropuesta = await _context.VotoPropuesta.FindAsync(id);
            if (votoPropuesta == null)
            {
                return NotFound();
            }
            ViewData["PropuestaId"] = new SelectList(_context.Propuesta, "PropuestaId", "Titulo", votoPropuesta.PropuestaId);
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "UserName", votoPropuesta.UsuarioId);
            return View(votoPropuesta);
        }

        // POST: VotoPropuestas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VotoPropuestaId,PropuestaId,Votacion,UsuarioId,Comentario")] VotoPropuesta votoPropuesta)
        {
            if (id != votoPropuesta.VotoPropuestaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(votoPropuesta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VotoPropuestaExists(votoPropuesta.VotoPropuestaId))
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
            ViewData["PropuestaId"] = new SelectList(_context.Propuesta, "PropuestaId", "Titulo", votoPropuesta.PropuestaId);
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "UserName", votoPropuesta.UsuarioId);
            return View(votoPropuesta);
        }

        // GET: VotoPropuestas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {

                if (id == null)
                {
                    return NotFound();
                }

                var votoPropuesta = await _context.VotoPropuesta
                    .Include(v => v.Propuesta)
                    .Include(v => v.Usuario)
                    .FirstOrDefaultAsync(m => m.VotoPropuestaId == id);
                if (votoPropuesta == null)
                {
                    return NotFound();
                }
                else
                {
                    var votoPropuestaRemove = await _context.VotoPropuesta.FindAsync(id);
                    _context.VotoPropuesta.Remove(votoPropuestaRemove);
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

        //// POST: VotoPropuestas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var votoPropuesta = await _context.VotoPropuesta.FindAsync(id);
        //    _context.VotoPropuesta.Remove(votoPropuesta);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool VotoPropuestaExists(int id)
        {
            return _context.VotoPropuesta.Any(e => e.VotoPropuestaId == id);
        }
    }
}
