using Solution.DO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using data = Solution.DO.Objects;
using Solution.DAL.EF;
using Solution.DAL.Repository;
using System.Threading.Tasks;

namespace Solution.DAL
{
    public class Foro : ICRUD<data.Foro>
    {

        private RepositoryForo _repository = null;

        public Foro(SolutionDbContext solutionDbContext)
        {
            _repository = new RepositoryForo(solutionDbContext);
        }

        public void Delete(data.Foro t)
        {
            _repository.Delete(t);
            _repository.Commit();
        }

        public IEnumerable<data.Foro> GetAll()
        {
            return _repository.GetAll();
        }

        public data.Foro GetOneById(int id)
        {
            return _repository.GetOneById(id);
        }
        public data.Foro GetOneById(String id)
        {
            return _repository.GetOneById(id);
        }

        public void Insert(data.Foro t)
        {
            _repository.Insert(t);
            _repository.Commit();
        }

        public void Update(data.Foro t)
        {
            _repository.Update(t);
            _repository.Commit();
        }

        public async Task<IEnumerable<data.Foro>> GetAllWithAsAsync()
        {
            return await _repository.GetAllWithAsAsync();
        }

        public async Task<data.Foro> GetOneWithAsAsync(int id)
        {
            return await _repository.GetOneWithAsAsync(id);
        }
        public async Task<data.Foro> GetOneWithAsAsyncPropuesta(int id)
        {
            return await _repository.GetOneWithAsAsyncPropuesta(id);
        }
        
    }
}