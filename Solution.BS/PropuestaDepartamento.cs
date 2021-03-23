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

        public async Task<data.PropuestaDepartamento> GetOneByIDsAsync(int PropuestaId, int DepartamentoId)
        {
            return await new DAL.PropuestaDepartamento(_repo).GetOneByIDsAsync(PropuestaId, DepartamentoId);

        }

        public void Delete(data.PropuestaDepartamento t)
        {
            new DAL.PropuestaDepartamento(_repo).Delete(t);
        }

        public IEnumerable<data.PropuestaDepartamento> GetAll()
        {
            return new DAL.PropuestaDepartamento(_repo).GetAll();
        }

        public data.PropuestaDepartamento GetOneById(int id)
        {
            return new DAL.PropuestaDepartamento(_repo).GetOneById(id);
        }

        public void Insert(data.PropuestaDepartamento t)
        {
            new DAL.PropuestaDepartamento(_repo).Insert(t);
        }

        // No se ocupa actualizar

        public void Update(data.PropuestaDepartamento t)
        {
            throw new NotImplementedException();
        }



        public async Task<IEnumerable<data.PropuestaDepartamento>> GetAllPropuestasByDepartamentoAsync(int id)
        {
            return await new DAL.PropuestaDepartamento(_repo).GetAllPropuestasByDepartamentoAsync(id);
        }

        public async Task<IEnumerable<data.PropuestaDepartamento>> GetAllWithAsAsync()
        {
            return await new DAL.PropuestaDepartamento(_repo).GetAllWithAsAsync();
        }




    }
}
