using Solution.DAL.EF;
using Solution.DAL.Repository;
using Solution.DO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = Solution.DO.Objects;

namespace Solution.DAL
{
     public class Comentario : ICRUD<data.Comentario>
    {
        private RepositoryComentario _repo = null;

        public Comentario(SolutionDbContext solutionDbContext)
        {
            _repo = new RepositoryComentario(solutionDbContext);
        }

        public void Delete(data.Comentario t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.Comentario> GetAll()
        {
            return _repo.GetAll();
        }

        public data.Comentario GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.Comentario t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.Comentario t)
        {
            _repo.Update(t);
            _repo.Commit();
        }

        public async Task<IEnumerable<data.Comentario>> GetAllWithAsync()
        {
            return await _repo.GetAllWithAsync();

        }

        public async Task<data.Comentario> GetOneWithAsync(int id)
        {
            return await _repo.GetOneWithAsync(id);

        }

        public async Task<IEnumerable<data.Comentario>> GetComentariosByForoAsync(int id)
        {
            return await _repo.GetComentariosByForoAsync(id);
        }


    }
}
