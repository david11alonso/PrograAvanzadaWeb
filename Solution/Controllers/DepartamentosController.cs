using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using datamodels = Solution.API.DataModels;
using Solution.DAL.EF;
using data = Solution.DO.Objects;

namespace Solution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {

        private readonly SolutionDbContext _context;

        private readonly IMapper _mapper;

        public DepartamentosController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }


        // GET: api/Departamentoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<datamodels.Departamento>>> GetDepartamento()
        {
            //Declaramos una variable para traer la informacion
            var aux = new Solution.BS.Departamento(_context).GetAll();
            // Casting

            var mapaux = _mapper.Map<IEnumerable<data.Departamento>, IEnumerable<datamodels.Departamento>>(aux).ToList();
            return mapaux;
        }

        // GET: api/Departamentoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<datamodels.Departamento>> GetDepartamento(int id)
        {
            var departamento = new Solution.BS.Departamento(_context).GetOneById(id);

            if (departamento == null)
            {
                return NotFound();
            }

            var mapaux = _mapper.Map<data.Departamento, datamodels.Departamento>(departamento);
            return mapaux;
        }

        // PUT: api/Departamentoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartamento(int id, datamodels.Departamento departamento)
        {
            if (id != departamento.DepartamentoId)
            {
                return BadRequest();
            }


            try
            {
                var mapaux = _mapper.Map<datamodels.Departamento, data.Departamento>(departamento);

                new Solution.BS.Departamento(_context).Update(mapaux);
            }
            catch (Exception ex)
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
        public async Task<ActionResult<datamodels.Departamento>> PostDepartamento(datamodels.Departamento departamento)
        {
            var mapaux = _mapper.Map<datamodels.Departamento, data.Departamento>(departamento);

            new Solution.BS.Departamento(_context).Insert(mapaux);

            return CreatedAtAction("GetDepartamento", new { id = departamento.DepartamentoId }, departamento);
        }

        // DELETE: api/Departamentoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<datamodels.Departamento>> DeleteDepartamento(int id)
        {
            var departamento = new Solution.BS.Departamento(_context).GetOneById(id);
            if (departamento == null)
            {
                return NotFound();
            }

            new Solution.BS.Departamento(_context).Delete(departamento);
            var mapaux = _mapper.Map<data.Departamento, datamodels.Departamento>(departamento);

            return mapaux;
        }

        private bool DepartamentoExists(int id)
        {
            return (new Solution.BS.Departamento(_context).GetOneById(id) != null);
        }


    }
}
