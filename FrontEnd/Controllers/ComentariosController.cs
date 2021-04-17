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
    public class ComentariosController : BaseController
    {
        private readonly PrograAvanzadaWebContext _context;

        public ComentariosController(PrograAvanzadaWebContext context)
        {
            _context = context;
        }

        // GET: Comentarios
        public async Task<IActionResult> Index()
        {
            var prograAvanzadaWebContext = _context.Comentario.Include(c => c.Foro).Include(c => c.Usuario);
            return View(await prograAvanzadaWebContext.ToListAsync());
        }

        // GET: Comentarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentario
                .Include(c => c.Foro)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.ComentarioId == id);
            if (comentario == null)
            {
                return NotFound();
            }

            return View(comentario);
        }

        // GET: Comentarios/Create
        public IActionResult Create()
        {
            ViewData["ForoId"] = new SelectList(_context.Foro, "ForoId", "ForoId");
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "UserName");
            return View();
        }

        // POST: Comentarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ForoId,UsuarioId,Comentario1,ComentarioId")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comentario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ForoId"] = new SelectList(_context.Foro, "ForoId", "ForoId", comentario.ForoId);
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "UserName", comentario.UsuarioId);
            return View(comentario);
        }

        // GET: Comentarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentario.FindAsync(id);
            if (comentario == null)
            {
                return NotFound();
            }
            ViewData["ForoId"] = new SelectList(_context.Foro, "ForoId", "ForoId", comentario.ForoId);
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "UserName", comentario.UsuarioId);
            return View(comentario);
        }

        // POST: Comentarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ForoId,UsuarioId,Comentario1,ComentarioId")] Comentario comentario)
        {
            if (id != comentario.ComentarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comentario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentarioExists(comentario.ComentarioId))
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
            ViewData["ForoId"] = new SelectList(_context.Foro, "ForoId", "ForoId", comentario.ForoId);
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "UserName", comentario.UsuarioId);
            return View(comentario);
        }

        // GET: Comentarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var comentario = await _context.Comentario
                    .Include(c => c.Foro)
                    .Include(c => c.Usuario)
                    .FirstOrDefaultAsync(m => m.ComentarioId == id);

                if (comentario == null)
                {
                    return NotFound();
                }
                else
                {
                    var comentarioRemove = await _context.Comentario.FindAsync(id);
                    _context.Comentario.Remove(comentarioRemove);
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

        // POST: Comentarios/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var comentario = await _context.Comentario.FindAsync(id);
        //    _context.Comentario.Remove(comentario);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ComentarioExists(int id)
        {
            return _context.Comentario.Any(e => e.ComentarioId == id);
        }
    }
}
