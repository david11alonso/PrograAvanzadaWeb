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
    public class Noticia : ICRUD<data.Noticia>
    {
        private RepositoryNoticia _repo = null;

        public Noticia(SolutionDbContext solutionDbContext)
        {
            _repo = new RepositoryNoticia(solutionDbContext);
        }

        public void Delete(data.Noticia t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.Noticia> GetAll()
        {
            return _repo.GetAll();
        }

        public data.Noticia GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.Noticia t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.Noticia t)
        {
            _repo.Update(t);
            _repo.Commit();
        }

        public async Task<IEnumerable<data.Noticia>> GetAllWithAsync()
        {
            return await _repo.GetAllWithAsync();

        }

        public async Task<data.Noticia> GetOneWithAsync(int id)
        {
            return await _repo.GetOneWithAsync(id);

        }
    }
}
