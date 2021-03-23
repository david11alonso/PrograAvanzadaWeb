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
    public class PropuestaDepartamento : ICRUD<data.PropuestaDepartamento>
    {
        private RepositoryPropuestaDepartamento _repo = null;

        public PropuestaDepartamento(SolutionDbContext solutionDbContext)
        {
            _repo = new RepositoryPropuestaDepartamento(solutionDbContext);
        }

        public void Delete(data.PropuestaDepartamento t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<data.PropuestaDepartamento> GetAll()
        {
            throw new NotImplementedException();
        }

        public data.PropuestaDepartamento GetOneById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(data.PropuestaDepartamento t)
        {
            throw new NotImplementedException();
        }

        public void Update(data.PropuestaDepartamento t)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<data.PropuestaDepartamento>> GetAllWithAsync()
        {
            return await _repo.GetAllWithAsync();
        }

        public async Task<IEnumerable<data.PropuestaDepartamento>> GetOneByIdWithAsync(int id)
        {
            return await _repo.GetOneWithAsync(id);
        }

    }
}
