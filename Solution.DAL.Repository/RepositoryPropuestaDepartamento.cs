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
    public class RepositoryPropuestaDepartamento : Repository<data.PropuestaDepartamento>, IRepositoryPropuestaDepartamento
    {
        public RepositoryPropuestaDepartamento(SolutionDbContext context) : base(context)
        {

        }

        private SolutionDbContext _db
        {
            get { return dbContext as SolutionDbContext; }
        }

        public async Task<IEnumerable<PropuestaDepartamento>> GetAllPropuestasByDepartamentoAsync(int id)
        {
            return await _db.PropuestaDepartamento
                .Include(m => m.Propuesta)
                .Include(m => m.Departamento)
                .Where(m => m.DepartamentoId == id).ToListAsync();
        }

        public async Task<IEnumerable<PropuestaDepartamento>> GetAllWithAsAsync()
        {
            return await _db.PropuestaDepartamento
               .Include(m => m.Propuesta)
               .Include(m => m.Departamento)
               .ToListAsync();
        }

        public async Task<PropuestaDepartamento> GetOneByIDsAsync(int PropuestaId, int DepartamentoId)
        {
            return await _db.PropuestaDepartamento
                .Include(m => m.Propuesta)
                .Include(m => m.Departamento)
                .SingleOrDefaultAsync(m => m.PropuestaId == PropuestaId && m.DepartamentoId == DepartamentoId);
        }
    }
}
