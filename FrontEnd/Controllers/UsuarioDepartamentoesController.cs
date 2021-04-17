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
    public class UsuarioDepartamentoesController : BaseController
    {
        private readonly PrograAvanzadaWebContext _context;

        public UsuarioDepartamentoesController(PrograAvanzadaWebContext context)
        {
            _context = context;
        }

        // GET: UsuarioDepartamentoes
        public async Task<IActionResult> Index()
        {
            var prograAvanzadaWebContext = _context.UsuarioDepartamento.Include(u => u.Departamento).Include(u => u.Usuario);
            return View(await prograAvanzadaWebContext.ToListAsync());
        }

        // GET: UsuarioDepartamentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioDepartamento = await _context.UsuarioDepartamento
                .Include(u => u.Departamento)
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(m => m.UsuarioDepartamentoId == id);
            if (usuarioDepartamento == null)
            {
                return NotFound();
            }

            return View(usuarioDepartamento);
        }

        // GET: UsuarioDepartamentoes/Create
        public IActionResult Create()
        {
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Nombre");
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: UsuarioDepartamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioDepartamentoId,UsuarioId,DepartamentoId")] UsuarioDepartamento usuarioDepartamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioDepartamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Nombre", usuarioDepartamento.DepartamentoId);
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "Id", usuarioDepartamento.UsuarioId);
            return View(usuarioDepartamento);
        }

        // GET: UsuarioDepartamentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioDepartamento = await _context.UsuarioDepartamento.FindAsync(id);
            if (usuarioDepartamento == null)
            {
                return NotFound();
            }
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Nombre", usuarioDepartamento.DepartamentoId);
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "Id", usuarioDepartamento.UsuarioId);
            return View(usuarioDepartamento);
        }

        // POST: UsuarioDepartamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuarioDepartamentoId,UsuarioId,DepartamentoId")] UsuarioDepartamento usuarioDepartamento)
        {
            if (id != usuarioDepartamento.UsuarioDepartamentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioDepartamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioDepartamentoExists(usuarioDepartamento.UsuarioDepartamentoId))
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
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Nombre", usuarioDepartamento.DepartamentoId);
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "Id", usuarioDepartamento.UsuarioId);
            return View(usuarioDepartamento);
        }

        // GET: UsuarioDepartamentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var usuarioDepartamento = await _context.UsuarioDepartamento
                    .Include(u => u.Departamento)
                    .Include(u => u.Usuario)
                    .FirstOrDefaultAsync(m => m.UsuarioDepartamentoId == id);
                if (usuarioDepartamento == null)
                {
                    return NotFound();
                }
                else {
                    var usuarioDepartamentoRemove = await _context.UsuarioDepartamento.FindAsync(id);
                    _context.UsuarioDepartamento.Remove(usuarioDepartamentoRemove);
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

        // POST: UsuarioDepartamentoes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var usuarioDepartamento = await _context.UsuarioDepartamento.FindAsync(id);
        //    _context.UsuarioDepartamento.Remove(usuarioDepartamento);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool UsuarioDepartamentoExists(int id)
        {
            return _context.UsuarioDepartamento.Any(e => e.UsuarioDepartamentoId == id);
        }
    }
}
