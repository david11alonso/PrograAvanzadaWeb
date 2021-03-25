using System;
using System.Collections.Generic;
using System.Text;
using Solution.DAL.EF;
using Solution.DO.Interfaces;
using System.Threading.Tasks;
using data = Solution.DO.Objects;

namespace Solution.BS
{
    public class Noticia : ICRUD<data.Noticia>
    {

        private SolutionDbContext _repo = null;

        public Noticia(SolutionDbContext solutionDbContext)
        {
            _repo = solutionDbContext;
        }

        public void Delete(data.Noticia t)
        {
            new DAL.Noticia(_repo).Delete(t);
        }

        public IEnumerable<data.Noticia> GetAll()
        {
            return new DAL.Noticia(_repo).GetAll();
        }

        public data.Noticia GetOneById(int id)
        {
            return new DAL.Noticia(_repo).GetOneById(id);
        }

        public void Insert(data.Noticia t)
        {
            new DAL.Noticia(_repo).Insert(t);
        }

        public void Update(data.Noticia t)
        {
            new DAL.Noticia(_repo).Update(t);
        }

        public async Task<IEnumerable<data.Noticia>> GetAllWithAsync()
        {
            return await new DAL.Noticia(_repo).GetAllWithAsync();

        }

        public async Task<data.Noticia> GetOneWithAsync(int id)
        {
            return await new DAL.Noticia(_repo).GetOneWithAsync(id);

        }
    }
}
