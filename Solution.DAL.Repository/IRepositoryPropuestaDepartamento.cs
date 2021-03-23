using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = Solution.DO.Objects;

namespace Solution.DAL.Repository
{
    public interface IRepositoryPropuestaDepartamento
    {
        Task<IEnumerable<data.PropuestaDepartamento>> GetAllPropuestasByDepartamentoAsync(int id);
        Task<IEnumerable<data.PropuestaDepartamento>> GetAllWithAsAsync();
        Task<data.PropuestaDepartamento> GetOneByIDsAsync(int PropuestaId, int DepartamentoId);
    }
}
