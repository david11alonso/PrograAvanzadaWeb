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
    public class NoticiaController : ControllerBase
    {
        private readonly SolutionDbContext _context;

        // Declaracion del automapper para poder castear los objetos

        private readonly IMapper _mapper;

        public NoticiaController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        // GET: api/Noticia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<datamodels.Noticia>>> GetNoticia()
        {
            //Declaramos una variable para traer la informacion
            var aux = await new Solution.BS.Noticia(_context).GetAllWithAsync();

            // Casting

            var mapaux = _mapper.Map<IEnumerable<data.Noticia>, IEnumerable<datamodels.Noticia>>(aux).ToList();
            return mapaux;
        }

        // GET: api/Noticia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<datamodels.Noticia>> GetNoticia(int id)
        {
            var noticia = await new Solution.BS.Noticia(_context).GetOneWithAsync(id);
            var mapaux = _mapper.Map<data.Noticia, datamodels.Noticia>(noticia);

            if (noticia == null)
            {
                return NotFound();
            }

            return mapaux;
        }

        // PUT: api/Noticia/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNoticia(int id, datamodels.Noticia noticia)
        {
            if (id != noticia.NoticiaId)
            {
                return BadRequest();
            }

            try
            {
                var mapaux = _mapper.Map<datamodels.Noticia, data.Noticia>(noticia);

                new Solution.BS.Noticia(_context).Update(mapaux);
            }
            catch (Exception ex)
            {
                if (!NoticiaExists(id))
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

        // POST: api/Noticia
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<datamodels.Noticia>> PostNoticia(datamodels.Noticia noticia)
        {
            var mapaux = _mapper.Map<datamodels.Noticia, data.Noticia>(noticia);
            new Solution.BS.Noticia(_context).Insert(mapaux);


            return CreatedAtAction("GetNoticia", new { id = noticia.NoticiaId }, noticia);
        }

        // DELETE: api/Noticia/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<datamodels.Noticia>> DeleteNoticia(int id)
        {
            var noticia = new Solution.BS.Noticia(_context).GetOneById(id);
            if (noticia == null)
            {
                return NotFound();
            }

            new Solution.BS.Noticia(_context).Delete(noticia);
            var mapaux = _mapper.Map<data.Noticia, datamodels.Noticia>(noticia);

            return mapaux;
        }

        private bool NoticiaExists(int id)
        {
            return (new Solution.BS.Noticia(_context).GetOneById(id) != null);
        }
    }
}
