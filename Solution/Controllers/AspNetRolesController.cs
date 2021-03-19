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
    public class AspNetRolesController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;


        public AspNetRolesController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }


        // GET: api/AspNetRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<datamodels.AspNetRoles>>> GetAspNetUsers()
        {

            var aux = new Solution.BS.AspNetRoles(_context).GetAll();

            var mapaux = _mapper.Map<IEnumerable<data.AspNetRoles>, IEnumerable<datamodels.AspNetRoles>>(aux).ToList();

            return mapaux;

            //return await _context.AspNetRoles.ToListAsync();
        }

        // GET: api/AspNetRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<datamodels.AspNetRoles>> GetAspNetUsers(string id)
        {
            //var role = await _context.AspNetRoles.FindAsync(id);

            var aux = new Solution.BS.AspNetRoles(_context).GetOneById(id);

            var mapaux = _mapper.Map<data.AspNetRoles, datamodels.AspNetRoles>(aux);

            if (mapaux == null)
            {
                return NotFound();
            }

            return mapaux;
        }

        // PUT: api/AspNetRoles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAspNetUsers(string id, datamodels.AspNetRoles role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }

            try
            {

                var mapaux = _mapper.Map<datamodels.AspNetRoles, data.AspNetRoles>(role);

                new Solution.BS.AspNetRoles(_context).Update(mapaux);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUsersExists(id))
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

        // POST: api/AspNetRoles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<datamodels.AspNetRoles>> PostAspNetUsers(datamodels.AspNetRoles role)
        {
            var mapaux = _mapper.Map<datamodels.AspNetRoles, data.AspNetRoles>(role);

            new Solution.BS.AspNetRoles(_context).Insert(mapaux);

            return CreatedAtAction("GetAspNetUsers", new { id = role.Id }, role);
        }

        // DELETE: api/AspNetRoles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<datamodels.AspNetRoles>> DeleteAspNetUsers(string id)
        {
            var role = await _context.AspNetRoles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            new Solution.BS.AspNetRoles(_context).Delete(role);

            var mapaux = _mapper.Map<data.AspNetRoles, datamodels.AspNetRoles>(role);

            return mapaux;
        }

        private bool AspNetUsersExists(string id)
        {
            return (new Solution.BS.AspNetRoles(_context).GetOneById(id) != null);
        }
    }
}
