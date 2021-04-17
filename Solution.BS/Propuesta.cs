
using System.Collections.Generic;
using System.Threading.Tasks;
using Solution.DAL.EF;
using Solution.DAL.Repository;
using Solution.DO.Interfaces;
using data = Solution.DO.Objects;


namespace Solution.BS
{
    public class Propuesta : ICRUD<data.Propuesta>
    {

        private SolutionDbContext _repo = null;

        public Propuesta(SolutionDbContext solutionDbContext)
        {
            _repo = solutionDbContext;
        }

        public void Delete(data.Propuesta t)
        {
            new DAL.Propuesta(_repo).Delete(t);
        }

        public IEnumerable<data.Propuesta> GetAll()
        {
            return new DAL.Propuesta(_repo).GetAll();
        }

        public data.Propuesta GetOneById(int id)
        {
            return new DAL.Propuesta(_repo).GetOneById(id);
        }

        public void Insert(data.Propuesta t)
        {
            new DAL.Propuesta(_repo).Insert(t);
        }

        public void Update(data.Propuesta t)
        {
            new DAL.Propuesta(_repo).Update(t);
        }

        public async Task<IEnumerable<data.Propuesta>> GetAllWithAsync()
        {
            return await new DAL.Propuesta(_repo).GetAllWithAsync();
        }


        public async Task<data.Propuesta> GetOneByIdWithAsync(int id)
        {
            return await new DAL.Propuesta(_repo).GetOneWithAsync(id);
        }
        public async Task<IEnumerable<data.Propuesta>> GetAllWithAsyncPendiente()
        {
            return await new DAL.Propuesta(_repo).GetAllWithAsyncPendiente();
        }
        
    }
}
