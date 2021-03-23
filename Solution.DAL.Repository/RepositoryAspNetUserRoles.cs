using Microsoft.EntityFrameworkCore;
using Solution.DAL.EF;
using Solution.DO.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data = Solution.DO.Objects;

namespace Solution.DAL.Repository
{
    public class RepositoryAspNetUserRoles : Repository<data.AspNetUserRoles>, IRepositoryAspNetUserRoles
    {
        private readonly SolutionDbContext context;
        public RepositoryAspNetUserRoles(SolutionDbContext _context) : base(_context)
        {
            //context = _context;
        }
        public async Task<IEnumerable<AspNetUserRoles>> GetAllWithAsAsync()
        {
            return await _db.AspNetUserRoles
                .Include(m => m.Role)
                .Include(m => m.User)
                .ToListAsync();
        }

        public async Task<AspNetUserRoles> GetOneWithAsAsync(string id)
        {
            return await _db.AspNetUserRoles
                .Include(m => m.Role)
                .Include(m => m.User)
                .SingleOrDefaultAsync(m => m.RoleId == id || m.UserId == id);
        }

        private SolutionDbContext _db
        {
            get { return dbContext as SolutionDbContext; }
        }

        public async Task<IEnumerable<AspNetUserRoles>> GetAllRolesByUserAsync(string userId)
        {
            return await _db.AspNetUserRoles
                .Include(m => m.User)
                .Include(m => m.Role)
                .Where(m => m.UserId == userId).ToListAsync();
        }

        public async Task<AspNetUserRoles> GetOneByIDsAsync(string userId, string roleId)
        {
            return await _db.AspNetUserRoles
                .Include(m => m.User)
                .Include(m => m.Role)
                .SingleOrDefaultAsync(m => m.UserId == userId && m.RoleId == roleId);
        }

    }
}
