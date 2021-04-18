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
    public class RepositoryUsuarioDepartamento : Repository<data.UsuarioDepartamento>, IRepositoryUsuarioDepartamento
    {
        public RepositoryUsuarioDepartamento(SolutionDbContext context) : base(context)
        {

        }

        private SolutionDbContext _db
        {
            get { return dbContext as SolutionDbContext; }
        }

        public async Task<IEnumerable<UsuarioDepartamento>> GetAllUsuariosByDepartamentoAsync(string id)
        {
            return await _db.UsuarioDepartamento
                .Include(m => m.Usuario)
                .Include(m => m.Departamento)
                .Where(m => m.UsuarioId == id).ToListAsync();
        }

        public async Task<IEnumerable<UsuarioDepartamento>> GetAllWithAsAsync()
        {
            return await _db.UsuarioDepartamento
               .Include(m => m.Usuario)
               .Include(m => m.Departamento)
               .ToListAsync();
        }

        public async Task<UsuarioDepartamento> GetOneByIDsAsync(string UsuarioId, int DepartamentoId)
        {
            return await _db.UsuarioDepartamento
                .Include(m => m.Usuario)
                .Include(m => m.Departamento)
                .SingleOrDefaultAsync(m => m.UsuarioId == UsuarioId && m.DepartamentoId == DepartamentoId);
        }
    
    }
}
