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
    public class VotoPropuestaController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;
        public VotoPropuestaController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        // GET: api/VotoPropuesta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<datamodels.VotoPropuesta>>> GetVotoPropuesta()
        {

            var aux = await new Solution.BS.VotoPropuesta(_context).GetAllWithAsAsync();

            var mapaux = _mapper.Map<IEnumerable<data.VotoPropuesta>, IEnumerable<datamodels.VotoPropuesta>>(aux).ToList();

            return mapaux;

        }
        // GET: api/VotoPropuesta
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<datamodels.VotoPropuesta>>> GetVotosByPropuesta(int id)
        {

            var aux = await new Solution.BS.VotoPropuesta(_context).GetAllVotosByPropuestaAsync(id);

            var mapaux = _mapper.Map<IEnumerable<data.VotoPropuesta>, IEnumerable<datamodels.VotoPropuesta>>(aux).ToList();

            return mapaux;

        }



        // POST: api/Foro
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<datamodels.VotoPropuesta>> PostVotoPropuesta(datamodels.VotoPropuesta votoPropuesta)
        {
            var aux = await new Solution.BS.VotoPropuesta(_context).GetOneByIDsAsync(votoPropuesta.UsuarioId, votoPropuesta.PropuestaId);


            if (aux == null)
            {
                

                var mapaux = _mapper.Map<DataModels.VotoPropuesta, data.VotoPropuesta >(votoPropuesta);
                new Solution.BS.VotoPropuesta(_context).Insert(mapaux);

                return Ok();

            }
            else
            {
                return BadRequest("El dato ya existe");
            }

       
        }

        // DELETE: api/Foro/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<datamodels.VotoPropuesta>> DeleteVotoPropuesta(int id)
        {
            var votoPropuesta = new Solution.BS.VotoPropuesta(_context).GetOneById(id);


            if (votoPropuesta == null)
            {
                return NotFound();
            }

            new Solution.BS.VotoPropuesta(_context).Delete(votoPropuesta);

            var mapaux = _mapper.Map<data.VotoPropuesta, datamodels.VotoPropuesta>(votoPropuesta);


            return mapaux;
        }





    }
}
