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
    public class ForoController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;


        public ForoController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }


        // GET: api/Foro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<datamodels.Foro>>> GetForo()
        {

            var aux = await new Solution.BS.Foro(_context).GetAllWithAsAsync();

            var mapaux = _mapper.Map<IEnumerable<data.Foro>, IEnumerable<datamodels.Foro>>(aux).ToList();

            return mapaux;

            //return await _context.Foro.ToListAsync();
        }

        // GET: api/Foro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<datamodels.Foro>> GetForo(int id)
        {
            //var foro = await _context.Foro.FindAsync(id);

            var aux = await new Solution.BS.Foro(_context).GetOneWithAsAsync(id);

            var mapaux = _mapper.Map<data.Foro, datamodels.Foro>(aux);

            if (mapaux == null)
            {
                return NotFound();
            }

            return mapaux;
        }

        // PUT: api/Foro/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutForo(int id, datamodels.Foro foro)
        //{
        //    if (id != foro.ForoId)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {

        //        var mapaux = _mapper.Map<datamodels.Foro, data.Foro>(foro);

        //        new Solution.BS.Foro(_context).Update(mapaux);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserRoleExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Foro
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<datamodels.Foro>> PostForo(datamodels.Foro foro)
        {
            var mapaux = _mapper.Map<datamodels.Foro, data.Foro>(foro);
            var propuesta = await new Solution.BS.Foro(_context).GetOneWithAsAsyncPropuesta(mapaux.PropuestaId);
            if ( propuesta == null) { 
            new Solution.BS.Foro(_context).Insert(mapaux);

            return CreatedAtAction("GetForo", new { id = foro.ForoId }, foro);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Foro/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<datamodels.Foro>> DeleteForo(int id)
        {
            var foro = new Solution.BS.Foro(_context).GetOneById(id);


            if (foro == null)
            {
                return NotFound();
            }

            new Solution.BS.Foro(_context).Delete(foro);

            var mapaux = _mapper.Map<data.Foro, datamodels.Foro>(foro);


            return mapaux;

        }

        private bool UserRoleExists(int id)
        {
            return (new Solution.BS.Foro(_context).GetOneById(id) != null);
        }
    }
}
