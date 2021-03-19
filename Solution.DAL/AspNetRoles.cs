using Solution.DAL.EF;
using Solution.DAL.Repository;
using Solution.DO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using data = Solution.DO.Objects;


namespace Solution.DAL
{
    public class AspNetRoles : ICRUDAspNetUserRoles<data.AspNetRoles>
    {

        private RepositoryAspNetUsersRoles<data.AspNetRoles> _repository = null;

        public AspNetRoles(SolutionDbContext solutionDbContext)
        {
            _repository = new RepositoryAspNetUsersRoles<data.AspNetRoles>(solutionDbContext);
        }

        public void Delete(data.AspNetRoles t)
        {
            _repository.Delete(t);
            _repository.Commit();
        }

        public IEnumerable<data.AspNetRoles> GetAll()
        {
            return _repository.GetAll();
        }

        public data.AspNetRoles GetOneById(string id)
        {
            return _repository.GetOneById(id);
        }


        public void Insert(data.AspNetRoles t)
        {
            _repository.Insert(t);
            _repository.Commit();
        }

        public void Update(data.AspNetRoles t)
        {
            _repository.Update(t);
            _repository.Commit();
        }
    }
}