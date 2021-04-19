using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = Solution.DO.Objects;

namespace Solution.DAL.Repository
{
    public interface IRepositoryUsuarioDepartamento
    {
        Task<IEnumerable<data.UsuarioDepartamento>> GetAllUsuariosByDepartamentoAsync(string id);
        Task<IEnumerable<data.UsuarioDepartamento>> GetAllWithAsAsync();
        Task<data.UsuarioDepartamento> GetOneByIDsAsync(string UsuarioId, int DepartamentoId);
    }
}
