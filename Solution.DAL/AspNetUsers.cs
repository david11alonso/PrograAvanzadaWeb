using Solution.DAL.EF;
using Solution.DAL.Repository;
using Solution.DO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using data = Solution.DO.Objects;


namespace Solution.DAL
{
    public class AspNetUsers : ICRUDAspNetUserRoles<data.AspNetUsers>
    {

        private RepositoryAspNetUsersRoles<data.AspNetUsers> _repository = null;

        public AspNetUsers(SolutionDbContext solutionDbContext)
        {
            _repository = new RepositoryAspNetUsersRoles<data.AspNetUsers>(solutionDbContext);
        }

        public void Delete(data.AspNetUsers t)
        {
            _repository.Delete(t);
            _repository.Commit();
        }

        public IEnumerable<data.AspNetUsers> GetAll()
        {
            return _repository.GetAll();
        }

        public data.AspNetUsers GetOneById(string id)
        {
            return _repository.GetOneById(id);
        }


        public void Insert(data.AspNetUsers t)
        {
            _repository.Insert(t);
            _repository.Commit();
        }

        public void Update(data.AspNetUsers t)
        {
            _repository.Update(t);
            _repository.Commit();
        }
    }
}