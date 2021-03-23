using System;
using System.Collections.Generic;
using System.Text;
using Solution.DAL.EF;
using Solution.DO.Interfaces;
using System.Threading.Tasks;
using data = Solution.DO.Objects;

namespace Solution.BS
{
    public class UsuarioDepartamento : ICRUD<data.UsuarioDepartamento>
    {
        private SolutionDbContext _repo = null;

        public UsuarioDepartamento(SolutionDbContext solutionDbContext)
        {
            _repo = solutionDbContext;

        }

        public void Delete(data.UsuarioDepartamento t)
        {
            new DAL.UsuarioDepartamento(_repo).Delete(t);
        }

        public IEnumerable<data.UsuarioDepartamento> GetAll()
        {
            return new DAL.UsuarioDepartamento(_repo).GetAll();
        }

        public data.UsuarioDepartamento GetOneById(int id)
        {
            return new DAL.UsuarioDepartamento(_repo).GetOneById(id);
        }

        public void Insert(data.UsuarioDepartamento t)
        {
            new DAL.UsuarioDepartamento(_repo).Insert(t);
        }

        public void Update(data.UsuarioDepartamento t)
        {
            new DAL.UsuarioDepartamento(_repo).Update(t);
        }

        public async Task<IEnumerable<data.UsuarioDepartamento>> GetAllWithAsync()
        {

            return await new DAL.UsuarioDepartamento(_repo).GetAllWithAsync();
        }


        //public async Task<data.Enrollment> GetOneByIdWithAsync(int id)
        //{
        //    return await new DAL.Enrollment(_repo).GetOneByIdWithAsync(id);

        //}

    }
}
