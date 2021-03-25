using System;
using System.Collections.Generic;
using System.Text;
using Solution.DAL.EF;
using Solution.DO.Interfaces;
using System.Threading.Tasks;
using data = Solution.DO.Objects;

namespace Solution.BS
{
    public class Comentario : ICRUD<data.Comentario>
    {

        private SolutionDbContext _repo = null;

        public Comentario(SolutionDbContext solutionDbContext)
        {
            _repo = solutionDbContext;
        }

        public void Delete(data.Comentario t)
        {
            new DAL.Comentario(_repo).Delete(t);
        }

        public IEnumerable<data.Comentario> GetAll()
        {
            return new DAL.Comentario(_repo).GetAll();
        }

        public data.Comentario GetOneById(int id)
        {
            return new DAL.Comentario(_repo).GetOneById(id);
        }

        public void Insert(data.Comentario t)
        {
            new DAL.Comentario(_repo).Insert(t);
        }

        public void Update(data.Comentario t)
        {
            new DAL.Comentario(_repo).Update(t);
        }

        public async Task<IEnumerable<data.Comentario>> GetAllWithAsync()
        {
            return await new DAL.Comentario(_repo).GetAllWithAsync();

        }

        public async Task<data.Comentario> GetOneWithAsync(int id)
        {
            return await new DAL.Comentario(_repo).GetOneWithAsync(id);

        }

        public async Task<IEnumerable<data.Comentario>> GetComentariosByForoAsync(int id)
        {
            return await new DAL.Comentario(_repo).GetComentariosByForoAsync(id);
        }
    }
}
