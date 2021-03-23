using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solution.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using data = Solution.DO.Objects;
using datamodels = Solution.API.DataModels;

namespace Solution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropuestaDepartamentoController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;
        public PropuestaDepartamentoController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        // GET: api/PropuestaDepartamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<datamodels.PropuestaDepartamento>>> GetPropuestaDepartamento()
        {

            var aux = await new Solution.BS.PropuestaDepartamento(_context).GetAllWithAsAsync();

            var mapaux = _mapper.Map<IEnumerable<data.PropuestaDepartamento>, IEnumerable<datamodels.PropuestaDepartamento>>(aux).ToList();

            return mapaux;

        }
        // GET: api/PropuestaDepartamento
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<datamodels.PropuestaDepartamento>>> GetPropuestasByDepartamento(int id)
        {

            var aux = await new Solution.BS.PropuestaDepartamento(_context).GetAllPropuestasByDepartamentoAsync(id);

            var mapaux = _mapper.Map<IEnumerable<data.PropuestaDepartamento>, IEnumerable<datamodels.PropuestaDepartamento>>(aux).ToList();

            return mapaux;

        }



        // POST: api/PropuestaDepartamento
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<datamodels.PropuestaDepartamento>> PostPropuestaDepartamento(datamodels.PropuestaDepartamento propuestaDepartamento)
        {
            var aux = await new Solution.BS.PropuestaDepartamento(_context).GetOneByIDsAsync(propuestaDepartamento.PropuestaId, propuestaDepartamento.DepartamentoId);


            if (aux == null)
            {


                var mapaux = _mapper.Map<DataModels.PropuestaDepartamento, data.PropuestaDepartamento>(propuestaDepartamento);
                new Solution.BS.PropuestaDepartamento(_context).Insert(mapaux);

                return Ok();

            }
            else
            {
                return BadRequest("El dato ya existe");
            }


        }

        // DELETE: api/PropuestaDepartamento/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<datamodels.PropuestaDepartamento>> DeletePropuestaDepartamentoa(int id)
        {
            var propuestaDepartamento = new Solution.BS.PropuestaDepartamento(_context).GetOneById(id);


            if (propuestaDepartamento == null)
            {
                return NotFound();
            }

            new Solution.BS.PropuestaDepartamento(_context).Delete(propuestaDepartamento);

            var mapaux = _mapper.Map<data.PropuestaDepartamento, datamodels.PropuestaDepartamento>(propuestaDepartamento);


            return mapaux;
        }

    }
}
