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
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.PropuestaDepartamento> GetAll()
        {
            return _repo.GetAll();
        }

        public data.PropuestaDepartamento GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.PropuestaDepartamento t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.PropuestaDepartamento t)
        {
            _repo.Update(t);
            _repo.Commit();
        }


        public async Task<IEnumerable<data.PropuestaDepartamento>> GetAllPropuestasByDepartamentoAsync(int id)
        {
            return await _repo.GetAllPropuestasByDepartamentoAsync(id);
        }

        public async Task<IEnumerable<data.PropuestaDepartamento>> GetAllWithAsAsync()
        {
            return await _repo.GetAllWithAsAsync();
        }

        public async Task<data.PropuestaDepartamento> GetOneByIDsAsync(int PropuestaId, int DepartamentoId)
        {
            return await _repo.GetOneByIDsAsync(PropuestaId, DepartamentoId);

        }

    }
}
