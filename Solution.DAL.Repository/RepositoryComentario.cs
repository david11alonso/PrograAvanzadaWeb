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
    public class RepositoryComentario : Repository<data.Comentario>, IRepositoryComentario
    {
        private SolutionDbContext _db
        {
            get { return dbContext as SolutionDbContext; }
        }

        public RepositoryComentario(SolutionDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Comentario>> GetAllWithAsync()
        {
            return await _db.Comentario
                .Include(m => m.Usuario)
                .ToListAsync();
        }

        public async Task<Comentario> GetOneWithAsync(int id)
        {
            return await _db.Comentario
                .Include(m => m.Usuario)
                .SingleOrDefaultAsync(m => m.ComentarioId == id);
        }

        public async Task<IEnumerable<Comentario>> GetComentariosByForoAsync(int id)
        {
            return await _db.Comentario
                .Include(m => m.Foro)
                .Include(m => m.Usuario)
                .Where(m => m.ForoId == id).ToListAsync();
        }

    }
}
