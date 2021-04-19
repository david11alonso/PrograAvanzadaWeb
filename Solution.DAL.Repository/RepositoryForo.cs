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
    public class RepositoryForo : Repository<data.Foro>, IRepositoryForo
    {

        private readonly SolutionDbContext context;
        public RepositoryForo(SolutionDbContext _context) : base(_context)
        {
            //context = _context;
        }
        public async Task<IEnumerable<Foro>> GetAllWithAsAsync()
        {
            return await _db.Foro
                .Include(m => m.Propuesta)
                .ToListAsync();
        }

        public async Task<Foro> GetOneWithAsAsync(int id)
        {
            return await _db.Foro
                .Include(m => m.Propuesta)
                .SingleOrDefaultAsync(m => m.ForoId == id);
        }

        public async Task<Foro> GetOneWithAsAsyncPropuesta(int id)
        {
            return await _db.Foro
                .Include(m => m.Propuesta).
                SingleOrDefaultAsync(m => m.PropuestaId == id);
        }

        private SolutionDbContext _db
        {
            get { return dbContext as SolutionDbContext; }
        }
    }
}
