using Microsoft.EntityFrameworkCore;
using Solution.DAL.EF;
using Solution.DO.Objects;
using System;
using System.Collections.Generic;
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

        public async Task<AspNetUserRoles> GetOneWithAsAsync(String id)
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
    }
}
