using System;
using System.Collections.Generic;
using System.Text;
using data = Solution.DO.Objects;
using System.Threading.Tasks;
using Solution.DAL.EF;
using Solution.DAL.Repository;
using Solution.DO.Interfaces;

namespace Solution.DAL
{
    public class Propuesta : ICRUD<data.Propuesta>
    {

        private RepositoryPropuesta _repo = null;

        public Propuesta(SolutionDbContext solutionDbContext)
        {
            _repo = new RepositoryPropuesta(solutionDbContext);
        }

        public void Delete(data.Propuesta t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.Propuesta> GetAll()
        {
            return _repo.GetAll();
        }

        public data.Propuesta GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.Propuesta t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.Propuesta t)
        {
            _repo.Update(t);
            _repo.Commit();
        }

        public async Task<IEnumerable<data.Propuesta>> GetAllWithAsync()
        {
            return await _repo.GetAllWithAsync();

        }

        public async Task<data.Propuesta> GetOneWithAsync(int id)
        {
            return await _repo.GetOneWithAsync(id);

        }
        public async Task<IEnumerable<data.Propuesta>> GetAllWithAsyncPendiente()
        {
            return await _repo.GetAllWithAsyncPendiente();

        }
        
    }
}
