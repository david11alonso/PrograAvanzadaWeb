using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = Solution.DO.Objects;
using Microsoft.EntityFrameworkCore;
using Solution.DAL.EF;
using Solution.DO.Objects;

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

        public async Task<IEnumerable<UsuarioDepartamento>> GetAllWithAsync()
        {
            return await _db.UsuarioDepartamento
                .Include(m => m.Departamento)
                .Include(m => m.Usuario)
                .ToListAsync();
        }

        public async Task<UsuarioDepartamento> GetOneWithAsync(int id)
        {
            return await _db.UsuarioDepartamento
                .Include(m => m.Departamento)
                .Include(m => m.Usuario)
                .Where(m => m.UsuarioId.Equals(id)).ToListAsync();


        }
    }
}
