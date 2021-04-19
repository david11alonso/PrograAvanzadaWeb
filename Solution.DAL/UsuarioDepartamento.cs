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
    public class UsuarioDepartamento : ICRUD<data.UsuarioDepartamento>
    {

        private RepositoryUsuarioDepartamento _repo = null;


        public UsuarioDepartamento(SolutionDbContext solutionDbContext)
        {
            _repo = new RepositoryUsuarioDepartamento(solutionDbContext);
        }

        public void Delete(data.UsuarioDepartamento t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.UsuarioDepartamento> GetAll()
        {
            return _repo.GetAll();
        }

        public data.UsuarioDepartamento GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.UsuarioDepartamento t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.UsuarioDepartamento t)
        {
            _repo.Update(t);
            _repo.Commit();
        }

        public async Task<IEnumerable<data.UsuarioDepartamento>> GetAllUsuariosByDepartamentoAsync(string id)
        {
            return await _repo.GetAllUsuariosByDepartamentoAsync(id);

        }

        public async Task<IEnumerable<data.UsuarioDepartamento>> GetAllWithAsAsync()
        {
            return await _repo.GetAllWithAsAsync();

        }

        public async Task<data.UsuarioDepartamento> GetOneByIDsAsync(string UsuarioId, int DepartamentoId)
        {
            return await _repo.GetOneByIDsAsync(UsuarioId, DepartamentoId);

        }

    }

}
