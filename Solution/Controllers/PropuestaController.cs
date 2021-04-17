using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using data = Solution.DO.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Solution.DAL.EF;
using AutoMapper;
using datamodels = Solution.API.DataModels;


namespace Solution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropuestaController : ControllerBase
    {
        private readonly SolutionDbContext _context;

        // Declaracion del automapper para poder castear los objetos

        private readonly IMapper _mapper;

        public PropuestaController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/PropuestaController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<datamodels.Propuesta>>> GetPropuestaController()
        {
            //Declaramos una variable para traer la informacion
            var aux = await new Solution.BS.Propuesta(_context).GetAllWithAsync();

            // Casting

            var mapaux = _mapper.Map<IEnumerable<data.Propuesta>, IEnumerable<datamodels.Propuesta>>(aux).ToList();
            return mapaux;
        }
        [HttpGet("Pendiente")]
        public async Task<ActionResult<IEnumerable<datamodels.Propuesta>>> GetAllWithAsyncPendiente()
        {
            //Declaramos una variable para traer la informacion
            var aux = await new Solution.BS.Propuesta(_context).GetAllWithAsyncPendiente();

            // Casting

            var mapaux = _mapper.Map<IEnumerable<data.Propuesta>, IEnumerable<datamodels.Propuesta>>(aux).ToList();
            return mapaux;
        }
        [HttpGet("Aprobacion/{id}")]
        public async Task<ActionResult<IEnumerable<datamodels.Propuesta>>> Aprobar(int id)
        {
            var solution = new Solution.BS.Propuesta(_context);
            var propuesta =  solution.GetOneById(id);

            if(propuesta != null)
            {
                try
                {
                    propuesta.Pendiente = false;
                     solution.Update(propuesta);
                }
                catch (Exception ex)
                {
                    if (!PropuestaExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }

                return NoContent();
            }
            else
            {
                return NotFound();
            }
            

        }


        // GET: api/Propuesta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<datamodels.Propuesta>> GetPropuesta(int id)
        {
            var propuesta = await new Solution.BS.Propuesta(_context).GetOneByIdWithAsync(id);
            var mapaux = _mapper.Map<data.Propuesta, datamodels.Propuesta>(propuesta);

            if (propuesta == null)
            {
                return NotFound();
            }

            return mapaux;
        }

        // PUT: api/Propuesta/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPropuesta(int id, datamodels.Propuesta propuesta)
        {
            if (id != propuesta.PropuestaId)
            {
                return BadRequest();
            }

            try
            {
                var mapaux = _mapper.Map<datamodels.Propuesta, data.Propuesta>(propuesta);

                new Solution.BS.Propuesta(_context).Update(mapaux);
            }
            catch (Exception ex)
            {
                if (!PropuestaExists(id))
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

        // POST: api/GroupPropuesta
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<datamodels.Propuesta>> PostPropuesta(datamodels.Propuesta propuesta)
        {
            var mapaux = _mapper.Map<datamodels.Propuesta, data.Propuesta>(propuesta);
            new Solution.BS.Propuesta(_context).Insert(mapaux);


            return CreatedAtAction("GetPropuesta", new { id = propuesta.PropuestaId }, propuesta);
        }

        // DELETE: api/GroupPropuesta/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<datamodels.Propuesta>> DeletePropuesta(int id)
        {
            var propuesta = new Solution.BS.Propuesta(_context).GetOneById(id);
            if (propuesta == null)
            {
                return NotFound();
            }

            new Solution.BS.Propuesta(_context).Delete(propuesta);
            var mapaux = _mapper.Map<data.Propuesta, datamodels.Propuesta>(propuesta);


            return mapaux;
        }

        private bool PropuestaExists(int id)
        {
            return (new Solution.BS.Propuesta(_context).GetOneById(id) != null);
        }
    }
}
