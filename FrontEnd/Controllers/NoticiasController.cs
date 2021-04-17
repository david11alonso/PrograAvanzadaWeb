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
    public class NoticiasController : BaseController
    {
        private readonly PrograAvanzadaWebContext _context;

        public NoticiasController(PrograAvanzadaWebContext context)
        {
            _context = context;
        }

        // GET: Noticias
        public async Task<IActionResult> Index()
        {
            var prograAvanzadaWebContext = _context.Noticia.Include(n => n.Usuario);
            return View(await prograAvanzadaWebContext.ToListAsync());
        }

        // GET: Noticias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticia = await _context.Noticia
                .Include(n => n.Usuario)
                .FirstOrDefaultAsync(m => m.NoticiaId == id);
            if (noticia == null)
            {
                return NotFound();
            }

            return View(noticia);
        }

        // GET: Noticias/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "UserName");
            return View();
        }

        // POST: Noticias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoticiaId,UsuarioId,Descripcion")] Noticia noticia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noticia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "UserName", noticia.UsuarioId);
            return View(noticia);
        }

        // GET: Noticias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticia = await _context.Noticia.FindAsync(id);
            if (noticia == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "UserName", noticia.UsuarioId);
            return View(noticia);
        }

        // POST: Noticias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoticiaId,UsuarioId,Descripcion")] Noticia noticia)
        {
            if (id != noticia.NoticiaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noticia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoticiaExists(noticia.NoticiaId))
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
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "UserName", noticia.UsuarioId);
            return View(noticia);
        }

        // GET: Noticias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {

                if (id == null)
                {
                    return NotFound();
                }

                var noticia = await _context.Noticia
                    .Include(n => n.Usuario)
                    .FirstOrDefaultAsync(m => m.NoticiaId == id);
                if (noticia == null)
                {
                    return NotFound();
                }
                else
                {
                    var noticiaRemove = await _context.Noticia.FindAsync(id);
                    _context.Noticia.Remove(noticiaRemove);
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

        //// POST: Noticias/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var noticia = await _context.Noticia.FindAsync(id);
        //    _context.Noticia.Remove(noticia);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool NoticiaExists(int id)
        {
            return _context.Noticia.Any(e => e.NoticiaId == id);
        }
    }
}
