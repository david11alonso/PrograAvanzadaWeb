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

        public async Task<IEnumerable<data.UsuarioDepartamento>> GetAllWithAsync()
        {
            return await _repo.GetAllWithAsync();
        }

        public async Task<IEnumerable<data.UsuarioDepartamento>> GetOneByIdWithAsync(int id)
        {
            return await _repo.GetOneWithAsync(id);
        }

    }

}
