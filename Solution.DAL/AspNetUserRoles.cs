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
    public class AspNetUserRoles : ICRUD<data.AspNetUserRoles>
    {

        private RepositoryAspNetUserRoles _repository = null;

        public AspNetUserRoles(SolutionDbContext solutionDbContext)
        {
            _repository = new RepositoryAspNetUserRoles(solutionDbContext);
        }

        public void Delete(data.AspNetUserRoles t)
        {
            _repository.Delete(t);
            _repository.Commit();
        }

        public IEnumerable<data.AspNetUserRoles> GetAll()
        {
            return _repository.GetAll();
        }

        public data.AspNetUserRoles GetOneById(int id)
        {
            return _repository.GetOneById(id);
        }
        public data.AspNetUserRoles GetOneById(String id)
        {
            return _repository.GetOneById(id);
        }

        public void Insert(data.AspNetUserRoles t)
        {
            _repository.Insert(t);
            _repository.Commit();
        }

        public void Update(data.AspNetUserRoles t)
        {
            _repository.Update(t);
            _repository.Commit();
        }

        public async Task<IEnumerable<data.AspNetUserRoles>> GetAllWithAsAsync()
        {
            return await _repository.GetAllWithAsAsync();
        }

        public async Task<data.AspNetUserRoles> GetOneWithAsAsync(String id)
        {
            return await _repository.GetOneWithAsAsync(id);
        }
    }
}