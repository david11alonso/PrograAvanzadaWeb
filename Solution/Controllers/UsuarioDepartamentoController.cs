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
    public class UsuarioDepartamentoController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;
        public UsuarioDepartamentoController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        // GET: api/UsuarioDepartamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<datamodels.UsuarioDepartamento>>> GetUsuarioDepartamento()
        {

            var aux = await new Solution.BS.UsuarioDepartamento(_context).GetAllWithAsAsync();

            var mapaux = _mapper.Map<IEnumerable<data.UsuarioDepartamento>, IEnumerable<datamodels.UsuarioDepartamento>>(aux).ToList();

            return mapaux;

        }
        // GET: api/UsuarioDepartamento
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<datamodels.UsuarioDepartamento>>> GetUsuariosByDepartamento(string id)
        {

            var aux = await new Solution.BS.UsuarioDepartamento(_context).GetAllUsuariosByDepartamentoAsync(id);

            var mapaux = _mapper.Map<IEnumerable<data.UsuarioDepartamento>, IEnumerable<datamodels.UsuarioDepartamento>>(aux).ToList();

            return mapaux;

        }



        // POST: api/UsuarioDepartamento
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<datamodels.UsuarioDepartamento>> PostUsuarioDepartamento(datamodels.UsuarioDepartamento usuarioDepartamento)
        {
            var aux = await new Solution.BS.UsuarioDepartamento(_context).GetOneByIDsAsync(usuarioDepartamento.UsuarioId, usuarioDepartamento.DepartamentoId);


            if (aux == null)
            {


                var mapaux = _mapper.Map<DataModels.UsuarioDepartamento, data.UsuarioDepartamento>(usuarioDepartamento);
                new Solution.BS.UsuarioDepartamento(_context).Insert(mapaux);

                return Ok();

            }
            else
            {
                return BadRequest("El dato ya existe");
            }


        }

        // DELETE: api/UsuarioDepartamento/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<datamodels.UsuarioDepartamento>> DeleteUsuarioDepartamento(int id)
        {
            var usuarioDepartamento = new Solution.BS.UsuarioDepartamento(_context).GetOneById(id);


            if (usuarioDepartamento == null)
            {
                return NotFound();
            }

            new Solution.BS.UsuarioDepartamento(_context).Delete(usuarioDepartamento);

            var mapaux = _mapper.Map<data.UsuarioDepartamento, datamodels.UsuarioDepartamento>(usuarioDepartamento);


            return mapaux;
        }
    }
}
