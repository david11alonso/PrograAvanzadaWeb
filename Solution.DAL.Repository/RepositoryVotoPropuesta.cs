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
    public class RepositoryVotoPropuesta : Repository<data.VotoPropuesta>, IRepositoryVotoPropuesta
    {
        public RepositoryVotoPropuesta(SolutionDbContext context) : base(context)
        {

        }

        private SolutionDbContext _db
        {
            get { return dbContext as SolutionDbContext; }
        }
        public async Task<IEnumerable<VotoPropuesta>> GetAllVotosByPropuestaAsync(int id)
        {
            return await _db.VotoPropuesta
                .Include(m => m.Propuesta)
                .Include(m => m.Usuario)
                .Where(m => m.PropuestaId == id).ToListAsync();
        }

        public async Task<IEnumerable<VotoPropuesta>> GetAllWithAsAsync()
        {
            return await _db.VotoPropuesta
               .Include(m => m.Propuesta)
               .Include(m => m.Usuario)
               .ToListAsync();
        }

        public async Task<VotoPropuesta> GetOneByIDsAsync(string IDUsuario, int PropuestaId)
        {
            return await _db.VotoPropuesta
                .Include(m => m.Propuesta)
                .Include(m => m.Usuario)
                .SingleOrDefaultAsync(m => m.PropuestaId == PropuestaId && m.UsuarioId == IDUsuario);
        }
    }
}
