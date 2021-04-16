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
    public class AspNetUserRolesController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;


        public AspNetUserRolesController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }


        // GET: api/AspNetUserRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<datamodels.AspNetUserRoles>>> GetAspNetUserRoles()
        {

            var aux = await new Solution.BS.AspNetUserRoles(_context).GetAllWithAsAsync();

            var mapaux = _mapper.Map<IEnumerable<data.AspNetUserRoles>, IEnumerable<datamodels.AspNetUserRoles>>(aux).ToList();

            return mapaux;

            //return await _context.AspNetUserRoles.ToListAsync();
        }

        // GET: api/AspNetUserRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<datamodels.AspNetUserRoles>>> GetAllRolesByUser(string id)
        {
            //var userRole = await _context.AspNetUserRoles.FindAsync(id);

            var aux = await new Solution.BS.AspNetUserRoles(_context).GetAllRolesByUserAsync(id);

            var mapaux = _mapper.Map<IEnumerable<data.AspNetUserRoles>, IEnumerable< datamodels.AspNetUserRoles>>(aux).ToList();

            if (mapaux == null)
            {
                return NotFound();
            }

            return mapaux;
        }

        //// PUT: api/AspNetUserRoles/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAspNetUserRoles(String id, datamodels.AspNetUserRoles userRole)
        //{
        //    if (id != userRole.RoleId && id != userRole.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {

        //        var mapaux = _mapper.Map<datamodels.AspNetUserRoles, data.AspNetUserRoles>(userRole);

        //        new Solution.BS.AspNetUserRoles(_context).Update(mapaux);
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

        // POST: api/AspNetUserRoles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<datamodels.AspNetUserRoles>> PostAspNetUserRoles(datamodels.AspNetUserRoles userRole)
        {

            var aux = await new Solution.BS.AspNetUserRoles(_context).GetOneByIDsAsync(userRole.UserId, userRole.RoleId);


            if (aux == null)
            {


                var mapaux = _mapper.Map<DataModels.AspNetUserRoles, data.AspNetUserRoles>(userRole);
                new Solution.BS.AspNetUserRoles(_context).Insert(mapaux);

                return Ok();

            }
            else
            {
                return BadRequest("El dato ya existe");
            }
        }

        // DELETE: api/AspNetUserRoles/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<datamodels.AspNetUserRoles>> DeleteAspNetUserRoles(String id)
        //{
        //    var user = _context.AspNetUsers.Where(m => m.Id == id).FirstOrDefault();
        //    try
        //    {
        //        var userRole = await _context.AspNetUserRoles.FindAsync(id, _context.AspNetUserRoles.Where(m => m.User == user).FirstOrDefault().RoleId);

        //        if (userRole == null)
        //        {
        //            return NotFound();
        //        }

        //        new Solution.BS.AspNetUserRoles(_context).Delete(userRole);

        //        var mapaux = _mapper.Map<data.AspNetUserRoles, datamodels.AspNetUserRoles>(userRole);

        //        return mapaux;

        //    } catch (NullReferenceException)
        //    {
        //        return BadRequest();
        //    }
            
        //}

        //private bool UserRoleExists(String userId, String roleId)
        //{

        //    var user = new Solution.BS.AspNetUsers(_context).GetOneById(userId);
        //    var role = new Solution.BS.AspNetRoles(_context).GetOneById(roleId);

        //    if (user != null || role != null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}

        [HttpDelete]
        public async Task<ActionResult<datamodels.AspNetUserRoles>> DeleteAspNetUserRole(String userId, String roleId)
        {

            var userRole = await new Solution.BS.AspNetUserRoles(_context).GetOneByIDsAsync(userId, roleId);

            if (userRole == null)
            {
                return BadRequest();


            }

            new Solution.BS.AspNetUserRoles(_context).Delete(userRole);


            var mapaux = _mapper.Map<data.AspNetUserRoles, datamodels.AspNetUserRoles>(userRole);

            return mapaux;
        }

        // PUT: api/AspNetUserRoles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAspNetUserRoles(String userId, String roleId)
        //{

        //    try
        //    {
        //        if (UserRoleExists(userId, roleId))
        //        {
        //            var userRole = _context.AspNetUserRoles.Where(r => r.RoleId == roleId && r.UserId == userId).FirstOrDefault();

        //            var mapaux = _mapper.Map<data.AspNetUserRoles, datamodels.AspNetUserRoles>(userRole);

        //            new Solution.BS.AspNetUserRoles(_context).Update(userRole);

        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserRoleExists(userId, roleId))
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
            
    }
}
