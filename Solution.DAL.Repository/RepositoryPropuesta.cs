using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = Solution.DO.Objects;
using Microsoft.EntityFrameworkCore;
using Solution.DAL.EF;
using Solution.DO.Objects;
using System.Linq;

namespace Solution.DAL.Repository
{
    public class RepositoryPropuesta : Repository<data.Propuesta>, IRepositoryPropuesta
    {
        public RepositoryPropuesta(SolutionDbContext context) : base(context)
        {

        }

        private SolutionDbContext _db
        {
            get { return dbContext as SolutionDbContext; }
        }

        public async Task<IEnumerable<Propuesta>> GetAllWithAsync()
        {
            return await _db.Propuesta
                .Include(m => m.Usuario)
                .ToListAsync();
        }

        public async Task<IEnumerable<Propuesta>> GetAllWithAsyncPendiente()
        {
            return await _db.Propuesta
                .Include(m => m.Usuario)
                .Where(m => m.Pendiente == true)
                .ToListAsync();
        }

        public async Task<Propuesta> GetOneWithAsync(int id)
        {
            return await _db.Propuesta
                .Include(m => m.Usuario)
                .SingleOrDefaultAsync(m => m.PropuestaId == id);
        }
    }
}
