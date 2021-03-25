using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using data = Solution.DO.Objects;
using Solution.DAL.EF;
using AutoMapper;
using datamodels = Solution.API.DataModels;


namespace Solution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly SolutionDbContext _context;

        // Declaracion del automapper para poder castear los objetos

        private readonly IMapper _mapper;

        public ComentarioController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        // GET: api/Comentario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<datamodels.Comentario>>> GetComentario()
        {
            var aux = await new Solution.BS.Comentario(_context).GetAllWithAsync();

            var mapaux = _mapper.Map<IEnumerable<data.Comentario>, IEnumerable<datamodels.Comentario>>(aux).ToList();
            return mapaux;
        }

        // GET: api/Comentario/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<datamodels.Comentario>> GetComentario(int id)
        //{
        //    var comentario = await new Solution.BS.Comentario(_context).GetOneWithAsync(id);
        //    var mapaux = _mapper.Map<data.Comentario, datamodels.Comentario>(comentario);

        //    if (comentario == null)
        //    {
        //        return NotFound();
        //    }

        //    return mapaux;
        //}

        // GET: api/VotoPropuesta
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<datamodels.Comentario>>> GetComentariosByForo(int id)
        {

            var aux = await new Solution.BS.Comentario(_context).GetComentariosByForoAsync(id);

            var mapaux = _mapper.Map<IEnumerable<data.Comentario>, IEnumerable<datamodels.Comentario>>(aux).ToList();

            return mapaux;

        }

        // PUT: api/Comentario/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComentario(int id, datamodels.Comentario comentario)
        {
            if (id != comentario.ComentarioId)
            {
                return BadRequest();
            }

            try
            {
                var mapaux = _mapper.Map<datamodels.Comentario, data.Comentario>(comentario);

                new Solution.BS.Comentario(_context).Update(mapaux);
            }
            catch (Exception ex)
            {
                if (!ComentarioExists(id))
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

        // POST: api/Comentario
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<datamodels.Comentario>> PostComentario(datamodels.Comentario comentario)
        {
            var mapaux = _mapper.Map<datamodels.Comentario, data.Comentario>(comentario);
            new Solution.BS.Comentario(_context).Insert(mapaux);


            return CreatedAtAction("GetComentario", new { id = comentario.ComentarioId }, comentario);
        }

        // DELETE: api/Comentario/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<datamodels.Comentario>> DeleteComentario(int id)
        {
            var comentario = new Solution.BS.Comentario(_context).GetOneById(id);
            if (comentario == null)
            {
                return NotFound();
            }

            new Solution.BS.Comentario(_context).Delete(comentario);
            var mapaux = _mapper.Map<data.Comentario, datamodels.Comentario>(comentario);

            return mapaux;
        }

        private bool ComentarioExists(int id)
        {
            return (new Solution.BS.Comentario(_context).GetOneById(id) != null);
        }
    }
}
