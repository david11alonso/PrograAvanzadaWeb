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

        // No se ocupa actualizar

        public void Update(data.UsuarioDepartamento t)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<data.UsuarioDepartamento>> GetAllUsuariosByDepartamentoAsync(string id)
        {
            return await new DAL.UsuarioDepartamento(_repo).GetAllUsuariosByDepartamentoAsync(id);

        }

        public async Task<IEnumerable<data.UsuarioDepartamento>> GetAllWithAsAsync()
        {
            return await new DAL.UsuarioDepartamento(_repo).GetAllWithAsAsync();

        }

        public async Task<data.UsuarioDepartamento> GetOneByIDsAsync(string UsuarioId, int DepartamentoId)
        {
            return await new DAL.UsuarioDepartamento(_repo).GetOneByIDsAsync(UsuarioId, DepartamentoId);

        }

    }
}
