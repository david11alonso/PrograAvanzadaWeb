﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solution.API_W.Models;

namespace Solution.API_W.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoesController : ControllerBase
    {
        private readonly PrograAvanzadaWebContext _context;

        public DepartamentoesController(PrograAvanzadaWebContext context)
        {
            _context = context;
        }

        // GET: api/Departamentoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetDepartamento()
        {
            return await _context.Departamento.ToListAsync();
        }

        // GET: api/Departamentoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Departamento>> GetDepartamento(int id)
        {
            var departamento = await _context.Departamento.FindAsync(id);

            if (departamento == null)
            {
                return NotFound();
            }

            return departamento;
        }

        // PUT: api/Departamentoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartamento(int id, Departamento departamento)
        {
            if (id != departamento.DepartamentoId)
            {
                return BadRequest();
            }

            _context.Entry(departamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Departamentoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Departamento>> PostDepartamento(Departamento departamento)
        {
            _context.Departamento.Add(departamento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DepartamentoExists(departamento.DepartamentoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDepartamento", new { id = departamento.DepartamentoId }, departamento);
        }

        // DELETE: api/Departamentoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Departamento>> DeleteDepartamento(int id)
        {
            var departamento = await _context.Departamento.FindAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }

            _context.Departamento.Remove(departamento);
            await _context.SaveChangesAsync();

            return departamento;
        }

        private bool DepartamentoExists(int id)
        {
            return _context.Departamento.Any(e => e.DepartamentoId == id);
        }
    }
}
