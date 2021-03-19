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
    public class AspNetUsersController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;


        public AspNetUsersController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }


        // GET: api/AspNetUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<datamodels.AspNetUsers>>> GetAspNetUsers()
        {

            var aux = new Solution.BS.AspNetUsers(_context).GetAll();

            var mapaux = _mapper.Map<IEnumerable<data.AspNetUsers>, IEnumerable<datamodels.AspNetUsers>>(aux).ToList();

            return mapaux;

            //return await _context.AspNetUsers.ToListAsync();
        }

        // GET: api/AspNetUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<datamodels.AspNetUsers>> GetAspNetUsers(string id)
        {
            //var user = await _context.AspNetUsers.FindAsync(id);

            var aux = new Solution.BS.AspNetUsers(_context).GetOneById(id);

            var mapaux = _mapper.Map<data.AspNetUsers, datamodels.AspNetUsers>(aux);

            if (mapaux == null)
            {
                return NotFound();
            }

            return mapaux;
        }

        // PUT: api/AspNetUsers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAspNetUsers(string id, datamodels.AspNetUsers user)
        //{
        //    if (id != user.Id)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {

        //        var mapaux = _mapper.Map<datamodels.AspNetUsers, data.AspNetUsers>(user);

        //        new Solution.BS.AspNetUsers(_context).Update(mapaux);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AspNetUsersExists(id))
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

        // POST: api/AspNetUsers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<datamodels.AspNetUsers>> PostAspNetUsers(datamodels.AspNetUsers user)
        //{
        //    var mapaux = _mapper.Map<datamodels.AspNetUsers, data.AspNetUsers>(user);

        //    new Solution.BS.AspNetUsers(_context).Insert(mapaux);

        //    return CreatedAtAction("GetAspNetUsers", new { id = user.Id }, user);
        //}

        // DELETE: api/AspNetUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<datamodels.AspNetUsers>> DeleteAspNetUsers(string id)
        {
            var user = await _context.AspNetUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            new Solution.BS.AspNetUsers(_context).Delete(user);

            var mapaux = _mapper.Map<data.AspNetUsers, datamodels.AspNetUsers>(user);

            return mapaux;
        }

        private bool AspNetUsersExists(string id)
        {
            return (new Solution.BS.AspNetUsers(_context).GetOneById(id) != null);
        }
    }
}
