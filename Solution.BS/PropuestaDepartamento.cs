using System;
using System.Collections.Generic;
using System.Text;
using Solution.DAL.EF;
using Solution.DO.Interfaces;
using System.Threading.Tasks;
using data = Solution.DO.Objects;

namespace Solution.BS
{
    public class PropuestaDepartamento : ICRUD<data.PropuestaDepartamento>
    {
        private SolutionDbContext _repo = null;

        public PropuestaDepartamento(SolutionDbContext solutionDbContext)
        {
            _repo = solutionDbContext;

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

            return await new DAL.PropuestaDepartamento(_repo).GetAllWithAsync();
        }


        //public async Task<data.Enrollment> GetOneByIdWithAsync(int id)
        //{
        //    return await new DAL.Enrollment(_repo).GetOneByIdWithAsync(id);

        //}


    }
}
