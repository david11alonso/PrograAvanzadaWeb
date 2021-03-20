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
            throw new NotImplementedException();
        }

        public IEnumerable<data.Propuesta> GetAll()
        {
            throw new NotImplementedException();
        }

        public data.Propuesta GetOneById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(data.Propuesta t)
        {
            throw new NotImplementedException();
        }

        public void Update(data.Propuesta t)
        {
            throw new NotImplementedException();
        }
    }
}
