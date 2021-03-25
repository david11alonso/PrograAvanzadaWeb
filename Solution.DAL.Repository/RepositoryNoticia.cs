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
    public class RepositoryNoticia : Repository<data.Noticia>, IRepositoryNoticia
    {
        private SolutionDbContext _db
        {
            get { return dbContext as SolutionDbContext; }
        }

        public RepositoryNoticia(SolutionDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Noticia>> GetAllWithAsync()
        {
            return await _db.Noticia
                .Include(m => m.Usuario)
                .ToListAsync();
        }

        public async Task<Noticia> GetOneWithAsync(int id)
        {
            return await _db.Noticia
                .Include(m => m.Usuario)
                .SingleOrDefaultAsync(m => m.NoticiaId == id);
        }
    }
}
